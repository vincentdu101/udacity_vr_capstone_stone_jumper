using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour {

	public GameObject closeBtn;
	public GameObject positiveBtn;
	public GameObject negativeBtn;

	private GameDataModel.CharacterChoice activeChoice;
	private GameObject gameDataService;
	private GameObject[] controls;
	private GameObject[] characters;
	private GameObject message;
	private GameObject messageMenu;
	private GameObject player;
	private GameObject camera;
	private GameObject gameData;
	private MenuDataService menuDataService;
	private CharacterDataService characterDataService;
	private string characterName;

	// Use this for initialization
	void Start () {
		message = GameObject.FindGameObjectWithTag ("Dialogue");
		messageMenu = GameObject.FindGameObjectWithTag ("MessageMenu");
		player = GameObject.FindGameObjectWithTag ("Player");
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		gameDataService = GameObject.FindGameObjectWithTag ("GameData");
		menuDataService = gameDataService.GetComponent<MenuDataService> ();
		characterDataService = gameDataService.GetComponent<CharacterDataService> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Boolean IsBtnActive(string btn) {
		return Array.IndexOf(activeChoice.btns, btn) != -1;
	}
		
	private void ActivateMenuBtns() {
		menuDataService.ActivateOrDeactivateBtn (closeBtn, IsBtnActive("close"), activeChoice, activeChoice.close);
		menuDataService.ActivateOrDeactivateBtn (positiveBtn, IsBtnActive("positive"), activeChoice, activeChoice.positiveText);
		menuDataService.ActivateOrDeactivateBtn (negativeBtn, IsBtnActive("negative"), activeChoice, activeChoice.negativeText);                         
	}

	private void LookAtPlayer() {
		this.transform.LookAt (player.transform.position);
	}

	private void DisableAllCharacters() {
		characters = GameObject.FindGameObjectsWithTag ("Character");

		foreach (GameObject character in characters) {
			character.SetActive (false);
		}
	}

	private void DisableControls() {
		controls = GameObject.FindGameObjectsWithTag ("Controls");

		foreach (GameObject control in controls) {
			control.SetActive (false);
		}
	}

	private void DetermineCharacterChoice() {
		Debug.Log(this.name);
		if (this.name == "EricTheRed") {
			characterName = "Eric The Red";
			characterDataService.StartEricQuest (1);
			activeChoice = characterDataService.GetKeyFigureChoice ("EricTheRed");
		} else {
			activeChoice = characterDataService.GetRandomChoice();
		}
	}

	private void SetCharacterName() {
		if (characterName == null) {
			characterName = characterDataService.GetCharacterName ();
		}
	}

	public void PlayerContactStart() {
		DetermineCharacterChoice();
		SetCharacterName ();
		menuDataService.MoveMessageMenu (camera);
		menuDataService.ModifyMenuMessage (activeChoice);
		menuDataService.SetCharacterName (characterName);
		ActivateMenuBtns ();
		DisableAllCharacters ();
		LookAtPlayer ();
	}
}

	
