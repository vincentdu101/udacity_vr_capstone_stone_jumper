using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDataService : MonoBehaviour {

	private Dictionary<string, bool> orbsCollected;
	private Dictionary<string, bool> itemsCollected;
	private GameObject orbText;
	private GameObject inventoryText;

	void Start() {
		orbsCollected = new Dictionary<string, bool> ();
		itemsCollected = new Dictionary<string, bool> ();
		orbText = GameObject.FindGameObjectWithTag ("Orbs");
		inventoryText = GameObject.FindGameObjectWithTag ("Inventory");
	}

	private string GetOrbCount() {
		return orbsCollected.Keys.Count + "/3";
	}

	private string GetInventoryList() {
		string items = "";
		if (itemsCollected.Keys.Count == 0) {
			return items;
		} else {
			foreach (string item in itemsCollected.Keys) {
				items += item + ", ";
			}
			return items.Remove(items.Length - 2);
		}
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
		inventoryText.GetComponent<Text> ().text = "Inventory\n " + GetInventoryList();
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

	public void ClearAllItems() {
		orbsCollected.Clear ();
		itemsCollected.Clear ();
		orbText.GetComponent<Text> ().text = "Orbs Collected: 0/3";
		inventoryText.GetComponent<Text> ().text = "Inventory\n 0 Items";
	}

	public void RemoveItem(string item) {
		if (itemsCollected.ContainsKey (item)) {
			itemsCollected.Remove (item);
			Debug.Log ("Remove Item " + item);
			Debug.Log ("Remove " + GetInventoryList());
			inventoryText.GetComponent<Text> ().text = "Inventory\n " + GetInventoryList();
		}
	}
}
