using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {

	public string itemRequired;
	public string orbColor;

	private GameObject gameData;
	private InventoryDataService inventoryDataService;

	void Start() {
		gameData = GameObject.FindGameObjectWithTag ("GameData");
		inventoryDataService = gameData.GetComponent<InventoryDataService> ();
	}

	void Update() {

	}

	public void TryCollectingOrb() {
		if (inventoryDataService.IsItemFound (itemRequired)) {
			inventoryDataService.OrbFound (orbColor);
			this.gameObject.SetActive (false);
		}
	}
}
