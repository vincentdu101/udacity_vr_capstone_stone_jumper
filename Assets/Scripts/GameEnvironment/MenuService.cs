using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuService : Photon.PunBehaviour {

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
	private GameDataModel.CharacterChoice nextChoice;
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

	public void closeMenu() {
		reactivateAllCharacters ();
		reactivateAllControls();
		messageMenu.SetActive (false);
	}

	public Boolean IsBtnActive(string btn) {
		return Array.IndexOf(nextChoice.btns, btn) != -1;
	}

	private GameDataModel.CharacterChoice GetNextChoice(GameDataModel.CharacterChoice current, String choice) {
		if (choice == "positive") {
			return characterDataService.GetNextPositiveChoice (current);
		} else {
			return characterDataService.GetNextNegativeChoice (current);
		}
	}

	public void nonCloseResponse(string choice) {
		GameDataModel.CharacterChoice currentChoice = this.GetComponentInParent<CharacterChoiceService>().GetChoice();
		nextChoice = GetNextChoice (currentChoice, choice);

		GameObject closeBtn = GameObject.Find ("OkBtn");
		GameObject positiveBtn = GameObject.Find ("PositiveBtn");
		GameObject negativeBtn = GameObject.Find ("NegativeBtn");

		menuDataService.ActivateOrDeactivateBtn (closeBtn, IsBtnActive("close"), nextChoice, nextChoice.close);
		menuDataService.ActivateOrDeactivateBtn (positiveBtn, IsBtnActive("positive"), nextChoice, nextChoice.positiveText);
		menuDataService.ActivateOrDeactivateBtn (negativeBtn, IsBtnActive("negative"), nextChoice, nextChoice.negativeText);
		menuDataService.ModifyMenuMessage (nextChoice);

		if (nextChoice.itemGranted != null) {
			string item = nextChoice.itemGranted;
			inventoryDataService.ItemFound (nextChoice.itemGranted);
			gameMessage.GetComponent<Text>().text = "You are given a " + item + ".";
		}
	}

	public void StartGame() {
		menuDataService.StartGame (player);
		gameServer.StartGame ();
		player.transform.position = startingPoint.transform.position;
	}



}
