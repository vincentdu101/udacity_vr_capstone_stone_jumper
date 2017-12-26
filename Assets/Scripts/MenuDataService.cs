using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDataService : MonoBehaviour {

	private GameObject mainMenu;
	private GameObject messageMenu;
	private GameObject message;
	private GameObject name;
	private GameObject item;
	private CharacterChoiceService characterChoiceService;
	Vector3 menuStartBuffer = new Vector3(0.0f, 0.5f, 2.25f);
	Vector3 menuCameraBuffer = new Vector3(0.0f, 1.0f, 1.0f);

	int distance = 3;

	// Use this for initialization
	void Start () {
		name = GameObject.FindGameObjectWithTag ("Name");
		message = GameObject.FindGameObjectWithTag ("Dialogue");
		item = GameObject.FindGameObjectWithTag ("Item");
		mainMenu = GameObject.FindGameObjectWithTag ("MainMenu");
		messageMenu = GameObject.FindGameObjectWithTag ("MessageMenu");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame(GameObject player) {
		mainMenu.SetActive (false);
		messageMenu.SetActive (true);
		messageMenu.transform.position = player.transform.position + menuStartBuffer;
	}

	public void MoveMessageMenu(GameObject camera) {
		messageMenu.SetActive (true);
		messageMenu.transform.position = camera.transform.position + menuCameraBuffer + camera.transform.forward * distance;
		messageMenu.transform.rotation = new Quaternion (0.0f, camera.transform.rotation.y, 0.0f, camera.transform.rotation.w);
	}

	public void ActivateOrDeactivateBtn(GameObject btn, Boolean activate, 
										GameDataModel.CharacterChoice choice, string text) {
		if (btn == null) {
			return;
		}

		if (activate == true) {
			characterChoiceService = btn.GetComponentInChildren<CharacterChoiceService>();
			btn.GetComponentInChildren<Text> ().text = text;
			btn.SetActive (true);
			characterChoiceService.SetChoice (choice);
		} else {
			btn.SetActive (false);
		}
	}

	public void ModifyMenuMessage(GameDataModel.CharacterChoice nextChoice) {
		message.GetComponent<Text>().text = nextChoice.text;

		if (nextChoice.itemGranted != null) {
			string itemFound = nextChoice.itemGranted;
			item.SetActive (true);
			item.GetComponentInChildren<Text> ().text = "Item Granted: " + itemFound;
		} else {
			item.SetActive (false);
		}
	}

	public void SetCharacterName(string characterName) {
		name.GetComponent<Text> ().text = characterName + ":";
	}

}
