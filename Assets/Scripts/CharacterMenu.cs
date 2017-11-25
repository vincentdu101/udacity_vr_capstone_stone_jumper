using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour {

	// public GameObject message;
	// public GameObject messageMenu;
	public GameObject[] activeMenuBtns;
	//public GameObject player;
	//public GameObject camera;
	//public GameObject character;

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
		message.GetComponent<Text>().text = this.GetComponent<Text> ().text;
	}

	private void ActivateMenuBtns() {
		foreach (GameObject btn in activeMenuBtns) {
			if (btn) {
				btn.SetActive (true);
			}
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
