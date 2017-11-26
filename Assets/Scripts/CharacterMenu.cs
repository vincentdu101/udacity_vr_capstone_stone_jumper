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

	int distance = 5;

	// Use this for initialization
	void Start () {
		message = GameObject.FindGameObjectWithTag ("Dialogue");
		messageMenu = GameObject.FindGameObjectWithTag ("MessageMenu");
		player = GameObject.FindGameObjectWithTag ("Player");
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		gameDataService = GameObject.FindGameObjectWithTag ("GameData");
		activeChoice = gameDataService.GetComponent<CharacterDataService> ().GetRandomChoice();
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

	private void ActivateMenuBtns() {
		if (Array.IndexOf(activeChoice.btns, "close") != -1) {
			closeBtn.GetComponentInChildren<Text>().text = activeChoice.close;
			closeBtn.SetActive (true);
		} else if (Array.IndexOf(activeChoice.btns, "positive") != -1) {
			positiveBtn.GetComponentInChildren<Text>().text = activeChoice.positiveText;
			positiveBtn.SetActive (true);
		} else if (Array.IndexOf(activeChoice.btns, "negative") != -1) {
			negativeBtn.GetComponentInChildren<Text>().text = activeChoice.negativeText;
			negativeBtn.SetActive (true);
		}
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
		MovePlayerAndMessageMenu ();
		ModifyMenuMessage ();
		ActivateMenuBtns ();
		DisableAllCharacters ();
		DisableControls ();
		LookAtPlayer ();
	}
}
