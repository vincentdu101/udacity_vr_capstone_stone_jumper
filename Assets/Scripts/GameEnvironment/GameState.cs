using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class GameState : MonoBehaviour {

	public enum STATE {STARTED, STOPPED, RESETTED}; 
	private STATE startState;
	private Vector3 currentPosition;
	private float currentHealth = 100.0f;
	private Stone[] stonesCollected = new Stone[]{};
	private Timer timeLeft;
	private int totalTime = 1000000;

	public void StartGame() {
		currentHealth = 100.0f;
		startState = STATE.STARTED;
		timeLeft = new Timer (totalTime);
		stonesCollected = new Stone[]{ };
		currentPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		Debug.Log ("game started");
	}

}
