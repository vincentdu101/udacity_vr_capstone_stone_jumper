using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour {

	public GameObject closeBtn;
	public GameObject positiveBtn;
	public GameObject negativeBtn;

	private GameDataModel.Contact activeContact;
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
	private float soundDistance = 6.0f;

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
		handleSoundEffect ();
	}

	private void handleSoundEffect() {
		AudioSource audio = this.GetComponent<AudioSource> ();

		if (audio != null) {
			Vector3 playerDirection = player.transform.position - this.transform.position;

			if (playerDirection.magnitude < soundDistance && audio.isPlaying == false) {
				audio.Play ();
			} else if (playerDirection.magnitude >= soundDistance && audio.isPlaying == true) {
				audio.Stop ();
			}
		}
	}

	public Boolean IsChoiceActive(int index) {
		return activeContact.choices[index] == null;
	}
		
	private void ActivateMenuBtns() {
		menuDataService.ActivateOrDeactivateBtn (positiveBtn, IsChoiceActive(0), activeContact, 0);
		menuDataService.ActivateOrDeactivateBtn (negativeBtn, IsChoiceActive(1), activeContact, 1);    
		menuDataService.ActivateOrDeactivateBtn (closeBtn, IsChoiceActive(2), activeContact, 2);                     
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

	private void DetermineCharacterContact() {
		Debug.Log(this.name);
		if (this.name == "EricTheRed") {
			characterName = "Erik The Red";
			activeContact = characterDataService.GetKeyFigureChoice ("EricTheRed");
		} else if (this.name == "Illugi") {
			characterName = "Illugi";
			activeContact = characterDataService.GetKeyFigureChoice ("Illugi");
		} else if (this.name == "ShipCaptain") {
			characterName = "Ship Captain";
			activeContact = characterDataService.GetKeyFigureChoice ("ShipCaptain");
		} else if (this.name == "Glaumur") {
			characterName = "Glaumur";
			activeContact = characterDataService.GetKeyFigureChoice ("Glaumur");
		} else {
			activeContact = characterDataService.GetRandomContact();
		}
	}

	private void SetCharacterName() {
		if (characterName == null) {
			characterName = characterDataService.GetCharacterName ();
		}
	}

	public void PlayerContactStart() {
		DetermineCharacterContact();
		SetCharacterName ();
		menuDataService.MoveMessageMenu (camera);
		menuDataService.ModifyMenuMessage (activeContact);
		menuDataService.SetCharacterName (characterName);
		ActivateMenuBtns ();
		DisableAllCharacters ();
		LookAtPlayer ();
	}
}

	
