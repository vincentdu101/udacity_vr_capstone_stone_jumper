using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stone : MonoBehaviour {

	public string itemRequired;
	public string orbColor;

	private GameObject gameData;
	private GameObject gameMessage;
	private InventoryDataService inventoryDataService;

	void Start() {
		gameData = GameObject.FindGameObjectWithTag ("GameData");
		gameMessage = GameObject.FindGameObjectWithTag ("GameMessage");
		inventoryDataService = gameData.GetComponent<InventoryDataService> ();
	
		gameMessage.GetComponent<Text>().text = "No message";
	}

	void Update() {

	}

	public void TryCollectingOrb() {
		if (inventoryDataService.IsItemFound (itemRequired)) {
			inventoryDataService.OrbFound (orbColor);
			this.gameObject.SetActive (false);
			gameMessage.GetComponent<Text>().text = "You've collected the " + orbColor + " orb.";
		} else {
			gameMessage.GetComponent<Text>().text = "You need a " + itemRequired 
				+ " to collect the orb.";
		}
	}
}
