using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementService : MonoBehaviour {
	
	public enum StepLength {One, Two};
	public enum StepDirection {Up, Down, Left, Right};
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveUpPathOne() {
		player.transform.position = player.transform.position + new Vector3(0.0f, 0.0f, 3.0f);
	}

	public void MoveUpPathTwo() {
		player.transform.position = player.transform.position + new Vector3(0.0f, 0.0f, 4.0f);
	}

	public void MoveDownPathOne() {
		player.transform.position = player.transform.position + new Vector3(0.0f, 0.0f, -3.0f);
	}

	public void MoveDownPathTwo() {
		player.transform.position = player.transform.position + new Vector3(0.0f, 0.0f, -4.0f);
	}

	public void MoveLeftPathOne() {
		player.transform.position = player.transform.position + new Vector3(-3.0f, 0.0f, 0.0f);
	}

	public void MoveLeftPathTwo() {
		player.transform.position = player.transform.position + new Vector3(-4.0f, 0.0f, 0.0f);
	}

	public void MoveRightPathOne() {
		player.transform.position = player.transform.position + new Vector3(3.0f, 0.0f, 0.0f);
	}

	public void MoveRightPathTwo() {
		player.transform.position = player.transform.position + new Vector3(4.0f, 0.0f, 0.0f);
	}
}
