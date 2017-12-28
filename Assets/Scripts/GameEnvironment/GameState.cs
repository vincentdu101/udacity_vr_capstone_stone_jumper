using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class GameState : MonoBehaviour {

	public enum STATE {STARTED, STOPPED, RESETTED}; 
	private STATE startState;
	private GameObject mainMenu;
	private GameObject gameData;
	private GameObject player;
	private PlayerLifeService playerLifeService;
	private InventoryDataService inventoryDataService;
	private Timer timeLeft;
	private int totalTime = 1000000;

	public void StartGame() {
		mainMenu = GameObject.FindGameObjectWithTag ("MainMenu");
		player = GameObject.FindGameObjectWithTag ("Player");
		gameData = Gam

		currentHealth = 100.0f;
		startState = STATE.STARTED;
		timeLeft = new Timer (totalTime);
		stonesCollected = new Stone[]{ };
		currentPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		Debug.Log ("game started");
	}

}
