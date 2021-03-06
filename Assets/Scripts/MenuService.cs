﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuService: MonoBehaviour {
	public GameObject gameController;
	public GameObject mainMenu;
	public GameObject messageMenu;
	public GameObject player;
	public GameObject startingPoint;
	private GameServer gameServer;
	private GameObject[] characters;
	private GameObject[] controls;
	private GameObject gameData;
	private GameObject gameMessage;
	private GameDataModel.Contact nextContact;
	private MenuDataService menuDataService;
	private CharacterDataService characterDataService;
	private InventoryDataService inventoryDataService;

	// Use this for initialization
	void Start () {
		if (gameController) {
			gameServer = gameController.GetComponent<GameServer> ();
		}
		characters = GameObject.FindGameObjectsWithTag ("Character");
		controls = GameObject.FindGameObjectsWithTag ("Controls");
		gameData = GameObject.FindGameObjectWithTag ("GameData");
		gameMessage = GameObject.FindGameObjectWithTag ("GameMessage");
		menuDataService = gameData.GetComponent<MenuDataService> ();
		characterDataService = gameData.GetComponent <CharacterDataService> ();
		inventoryDataService = gameData.GetComponent<InventoryDataService> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void reactivateAllCharacters() {
		foreach (GameObject character in characters) {
			character.SetActive (true);
		}
	}

	private void reactivateAllControls() {
		foreach (GameObject control in controls) {
			control.SetActive (true);
		}
	}
	private GameDataModel.Contact GetNextChoice(GameDataModel.Contact current, int choice) {
		return characterDataService.GetNextChoice(current, choice);
	}

	public void closeMenu() {
		reactivateAllCharacters ();
		reactivateAllControls();
		messageMenu.SetActive (false);
	}

	public Boolean IsBtnActive(int choice) {
		return nextContact.choices.Length > choice;
	}

	public void nonCloseResponse(int choice) {
		GameDataModel.Contact currentContact = this.GetComponentInParent<CharacterContactService>().GetContact();
		nextContact = GetNextChoice (currentContact, choice);

		if (nextContact == null) {
			closeMenu();
			return;
		}

		menuDataService.ActivateMenuBtns (nextContact);
		menuDataService.ModifyMenuMessage (nextContact);

		if (nextContact.itemGranted != null) {
			string item = nextContact.itemGranted;
			inventoryDataService.ItemFound (nextContact.itemGranted);
			gameMessage.GetComponent<Text>().text = "You are given a " + item + ".";
		}
	}

	public void StartGame() {
		menuDataService.StartGame (player);
		gameServer.StartGame ();
		player.transform.position = startingPoint.transform.position;
	}



}
