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
	private GameObject gameStateObj;
	private GameState gameState;

	void Start() {
		gameData = GameObject.FindGameObjectWithTag ("GameData");
		gameMessage = GameObject.FindGameObjectWithTag ("GameMessage");
		gameStateObj = GameObject.FindGameObjectWithTag ("GameState");
		gameState = gameStateObj.GetComponent<GameState> ();
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

			if (inventoryDataService.HaveAllOrbsBeenFound ()) {
				gameState.EndGame ();
			}
		} else {
			gameMessage.GetComponent<Text>().text = "You need a " + itemRequired 
				+ " to collect the orb.";
		}
	}
}
