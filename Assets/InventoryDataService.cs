using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataService : MonoBehaviour {

	private Dictionary<string, bool> orbsCollected;
	private Dictionary<string, bool> itemsCollected;

	void Start() {
		orbsCollected = new Dictionary<string, bool> ();
		itemsCollected = new Dictionary<string, bool> ();
	}

	public void OrbFound(string color) {
		orbsCollected.Add (color, true);
	}

	public bool HaveAllOrbsBeenFound() {
		return orbsCollected.Keys.Count == 3;
	}

	public void ItemFound(string item) {
		itemsCollected.Add (item, true);
	}

	public bool IsItemFound(string item) {
		bool found;
		itemsCollected.TryGetValue (item, out found);
		return found == true;
	}
}
