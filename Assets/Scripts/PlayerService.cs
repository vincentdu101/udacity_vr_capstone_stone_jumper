using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : MonoBehaviour {

	private GameObject gameData;
	private GameObject gameStateObj;
	private GameState gameState;
	private PlayerLifeService playerLifeService;

	// Use this for initialization
	void Start () {
		gameData = GameObject.FindGameObjectWithTag ("GameData");
		gameStateObj = GameObject.FindGameObjectWithTag ("GameState");
		gameState = gameStateObj.GetComponent<GameState> ();
		playerLifeService = gameData.GetComponent<PlayerLifeService> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Traitors") {
			playerLifeService.lifeTakingHit ();

			if (playerLifeService.getLife () < 0.0) {
				gameState.PlayerDied ();
			}
		}
	}
}
