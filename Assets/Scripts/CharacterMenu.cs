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

	Vector3 menuCameraBuffer = new Vector3(0.0f, 1.0f, 1.0f);

	int distance = 3;

	// Use this for initialization
	void Start () {
		message = GameObject.FindGameObjectWithTag ("Dialogue");
		messageMenu = GameObject.FindGameObjectWithTag ("MessageMenu");
		player = GameObject.FindGameObjectWithTag ("Player");
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		gameDataService = GameObject.FindGameObjectWithTag ("GameData");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void MovePlayerAndMessageMenu() {
		messageMenu.SetActive (true);
		messageMenu.transform.position = camera.transform.position + menuCameraBuffer + camera.transform.forward * distance;
		messageMenu.transform.rotation = new Quaternion (0.0f, camera.transform.rotation.y, 0.0f, camera.transform.rotation.w);
	}

	private void ModifyMenuMessage() {
		message.GetComponent<Text>().text = activeChoice.text;
	}

	private void ActivateOrDeactivateBtn(GameObject btn, Boolean activate, 
										 GameDataModel.CharacterChoice choice, 
										 string text) {
		if (activate) {
			btn.GetComponentInChildren<Text> ().text = text;
			btn.SetActive (true);
			btn.GetComponentInChildren<GameDataModel.CharacterChoice>().clone(choice);
		} else {
			btn.SetActive (false);
		}
	}

	private Boolean IsBtnActive(string btn) {
		return Array.IndexOf(activeChoice.btns, btn) != -1;
	}

	private void ActivateMenuBtns() {
		ActivateOrDeactivateBtn (closeBtn, IsBtnActive("close"), activeChoice, activeChoice.close);
		ActivateOrDeactivateBtn (positiveBtn, IsBtnActive("positive"), activeChoice, activeChoice.positiveText);
		ActivateOrDeactivateBtn (negativeBtn, IsBtnActive("negative"), activeChoice, activeChoice.negativeText);                         
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

	public void PlayerContactStart() {
		activeChoice = gameDataService.GetComponent<CharacterDataService> ().GetRandomChoice();
		MovePlayerAndMessageMenu ();
		ModifyMenuMessage ();
		ActivateMenuBtns ();
		DisableAllCharacters ();
		DisableControls ();
		LookAtPlayer ();
	}
}
