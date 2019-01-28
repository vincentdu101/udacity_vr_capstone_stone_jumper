using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDataService : MonoBehaviour {
	private int totalBtns = 3;
	private GameObject mainMenu;
	private GameObject messageMenu;
	private GameObject message;
	private GameObject name;
	private GameObject item;
	private GameObject gameData;
	private GameObject gameStateObj;
	private GameState gameState;
	private CharacterContactService characterContactService;
	private InventoryDataService inventoryDataService;
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
		gameData = GameObject.FindGameObjectWithTag ("GameData");
		gameStateObj = GameObject.FindGameObjectWithTag ("GameState");
		gameState = gameStateObj.GetComponent<GameState> ();
		inventoryDataService = gameData.GetComponent<InventoryDataService> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private bool isRequirementMet(GameDataModel.Choice choice) {
		bool requirementMet = false;
		if (choice.requirement != null) {
			return inventoryDataService.IsItemFound (choice.requirement);
		}
		return requirementMet;
	}

	private bool isItemGone(GameDataModel.Choice choice) {
		bool itemGone = true;
		if (choice.itemGone != null) {
			return !inventoryDataService.IsItemFound (choice.itemGone);
		}
		return itemGone;
	}

	private bool TaskNotCompleted(string task) {
		return !gameState.IsTaskDone (task);
	}

	private bool CheckChoice(GameDataModel.Choice choice) {
		if (choice.requirement != "") {
			return isRequirementMet(choice) && TaskNotCompleted(choice.requirement);
		} else if (choice.itemGone != "") {
			return isItemGone(choice) && TaskNotCompleted(choice.itemGone);
		} else {
			return true;
		}
	}
	private GameObject[] GatherButtons() {
		GameObject[] btns = new GameObject[totalBtns];
		for (int x = 0; x < totalBtns; x++) {
			GameObject btn = GameObject.Find ("btn" + x);
			btns[x] = btn;
		}
		return btns;
	}

	private void ResetAllBtns(GameObject[] btns) {
		foreach (GameObject btn in btns) {
			if (btn != null) {
				btn.SetActive(false);
			} 
		}
	}

	private GameDataModel.Choice[] FilterOutContactInChoices(GameDataModel.Contact contact) {
		GameDataModel.Choice[] choices = new GameDataModel.Choice[totalBtns];
		// for (int x = 0; x < contact.choices.Length; x++) {
		// 	if (contact.choices[x].contacts)
		// }
		return choices;
	}
		
	public void ActivateMenuBtns(GameDataModel.Contact contact) {
		GameObject[] btns = GatherButtons();
		GameDataModel.Choice[] validChoices = FilterOutContactInChoices(contact);
		ResetAllBtns(btns);
		for (int x = 0; x < totalBtns; x++) {
			// if (validChoices[x] != null) {
				ActivateOrDeactivateBtn(btns[x], contact, x);
			// } else {
			// 	btns[x].SetActive(false);
			// }
		}                  
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

	public void ActivateOrDeactivateBtn(GameObject btn, GameDataModel.Contact contact, int choiceIndex) {
		if (btn == null) {
			return;
		}

		if ((contact.choices.Length - 1) < choiceIndex) {
			btn.SetActive(false);
			return;
		}

		GameDataModel.Choice choice = contact.choices[choiceIndex];
		// choice.RemoveDupContactId(contact.id);
		if (CheckChoice(choice)) {
			Debug.Log(choice.text);
			characterContactService = btn.GetComponentInChildren<CharacterContactService>();
			btn.GetComponentInChildren<Text> ().text = choice.text;
			btn.SetActive (true);
			characterContactService.SetContact (contact);
		} else {
			Debug.Log(CheckChoice(choice));
			btn.SetActive (false);
		}
	}

	public void ModifyMenuMessage(GameDataModel.Contact nextContact) {
		message.GetComponent<Text>().text = nextContact.text;

		if (nextContact.itemGranted != null) {
			string itemFound = nextContact.itemGranted;
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
