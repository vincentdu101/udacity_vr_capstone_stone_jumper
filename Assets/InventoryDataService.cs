using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDataService : MonoBehaviour {

	private Dictionary<string, bool> orbsCollected;
	private Dictionary<string, bool> itemsCollected;
	private GameObject orbText;

	void Start() {
		orbsCollected = new Dictionary<string, bool> ();
		itemsCollected = new Dictionary<string, bool> ();
		orbText = GameObject.FindGameObjectWithTag ("Orbs");
	}

	private string GetOrbCount() {
		return orbsCollected.Keys.Count + "/3";
	}

	public void OrbFound(string color) {
		orbsCollected.Add (color, true);
		orbText.GetComponent<Text> ().text = "Orbs Collected: " + GetOrbCount ();
	}

	public bool HaveAllOrbsBeenFound() {
		return orbsCollected.Keys.Count == 3;
	}

	public void ItemFound(string item) {
		itemsCollected.Add (item, true);
	}

	public bool IsItemFound(string item) {
		if (item != "none") {
			bool found;
			itemsCollected.TryGetValue (item, out found);
			return found == true;
		} else {
			return true;
		}
	}
}
