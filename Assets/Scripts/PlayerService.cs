using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : MonoBehaviour {

	private GameObject gameData;
	private PlayerLifeService playerLifeService; 

	// Use this for initialization
	void Start () {
		gameData = GameObject.FindGameObjectWithTag ("GameData");
		playerLifeService = gameData.GetComponent<PlayerLifeService> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Traitors") {
			playerLifeService.lifeTakingHit ();
		}
	}
}
