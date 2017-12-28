using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

	public enum STATE {STARTED, STOPPED, RESETTED}; 
	private STATE startState;
	private GameObject mainMenu;
	private GameObject gameData;
	private GameObject player;
	private GameObject gameMessage;
	private PlayerLifeService playerLifeService;
	private InventoryDataService inventoryDataService;
	private Timer timeLeft;
	private int totalTime = 1000000;

	void Start() {
		mainMenu = GameObject.FindGameObjectWithTag ("MainMenu");
		player = GameObject.FindGameObjectWithTag ("Player");
		gameData = GameObject.FindGameObjectWithTag ("GameData");
		gameMessage = GameObject.FindGameObjectWithTag ("GameMessage");
	
		playerLifeService = gameData.GetComponent<PlayerLifeService> ();
		inventoryDataService = gameData.GetComponent<InventoryDataService> ();
	}

	public void StartGame() {
		playerLifeService.resetLife ();
		inventoryDataService.ClearAllItems ();
		startState = STATE.STARTED;
		gameMessage.GetComponent<Text> ().text = "No message";
		timeLeft = new Timer (totalTime);
	}

	public void EndGame() {
		playerLifeService.resetLife ();
		inventoryDataService.ClearAllItems ();
		startState = STATE.RESETTED;
		timeLeft = null;
		mainMenu.GetComponentInChildren<Text> ().text = "Yay you've won the game\n" +
		"you've collected all 3 orbs!";
		gameMessage.GetComponent<Text> ().text = "You've Won!";
		player.transform.position = mainMenu.transform.position;
	}

}
