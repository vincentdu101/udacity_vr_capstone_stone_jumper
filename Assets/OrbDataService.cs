using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbDataService : MonoBehaviour {

	private Dictionary<string, bool> orbsCollected;

	void Start() {
		orbsCollected = new Dictionary<string, bool> ();
	}

	public void OrbFound(string color) {
		orbsCollected.Add (color, true);
	}

	public bool HaveAllOrbsBeenFound() {
		return orbsCollected.Keys.Count == 3;
	}
}
