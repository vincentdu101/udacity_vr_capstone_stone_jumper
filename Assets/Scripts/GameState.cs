using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

	public enum STATE {STARTED, STOPPED, RESETTED}; 
	private STATE startState;
	private GameObject resetPoint;
	private GameObject mainMenu;
	private GameObject menuTitle;
	private GameObject menuWhere;
	private GameObject gameData;
	private GameObject player;
	private GameObject gameMessage;
	private GameObject messageMenu;
	private PlayerLifeService playerLifeService;
	private InventoryDataService inventoryDataService;
	private Timer timeLeft;
	private int totalTime = 1000000;
	private Dictionary<string, bool> tasksAccomplished;

	void Start() {
		mainMenu = GameObject.FindGameObjectWithTag ("MainMenu");
		player = GameObject.FindGameObjectWithTag ("Player");
		gameData = GameObject.FindGameObjectWithTag ("GameData");
		gameMessage = GameObject.FindGameObjectWithTag ("GameMessage");
		messageMenu = GameObject.FindGameObjectWithTag ("MessageMenu");
		resetPoint = GameObject.Find ("ResetPoint");
		menuTitle = GameObject.Find ("Title");
		menuWhere = GameObject.Find ("Where");
		tasksAccomplished = new Dictionary <string, bool> ();
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
		mainMenu.SetActive (true);
		playerLifeService.resetLife ();
		inventoryDataService.ClearAllItems ();
		startState = STATE.RESETTED;
		timeLeft = null;
		menuTitle.GetComponent<Text> ().text = "You Win!!!"; 
		menuWhere.GetComponent<Text>().text = "Yay you've won the game " +
		"you've collected all 3 orbs!\nPlay Again?";
		gameMessage.GetComponent<Text> ().text = "You've Won!";
		player.transform.position = resetPoint.transform.position;
		tasksAccomplished.Clear ();
		messageMenu.SetActive (false);
	}

	public void PlayerDied() {
		mainMenu.SetActive (true);
		playerLifeService.resetLife ();
		inventoryDataService.ClearAllItems ();
		startState = STATE.RESETTED;
		timeLeft = null;
		menuTitle.GetComponent<Text> ().text = "You Lost!";
		menuWhere.GetComponent<Text>().text = "Oh no you died. Play Again?";
		gameMessage.GetComponent<Text> ().text = "You've Lost!";
		player.transform.position = resetPoint.transform.position;
		tasksAccomplished.Clear ();
		messageMenu.SetActive (false);
	}

	public void FinishTask(string task) {
		tasksAccomplished.Add (task, true);
	}

	public bool IsTaskDone(string task) {
		bool state = false;
		tasksAccomplished.TryGetValue (task, out state);
		return state == true;
	}

}
