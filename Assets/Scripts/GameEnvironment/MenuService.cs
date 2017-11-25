using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuService : Photon.PunBehaviour {

	public GameObject gameController;
	public GameObject mainMenu;
	public GameObject messageMenu;
	public GameObject player;
	public GameObject startingPoint;
	private GameServer gameServer;
	private GameObject[] characters;
	private GameObject[] controls;

	Vector3 menuCameraBuffer = new Vector3(0.0f, 0.5f, 3.25f);

	// Use this for initialization
	void Start () {
		if (gameController) {
			gameServer = gameController.GetComponent<GameServer> ();
		}
		characters = GameObject.FindGameObjectsWithTag ("Character");
		controls = GameObject.FindGameObjectsWithTag ("Controls");
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void reactivateAllCharacters() {
		foreach (GameObject character in characters) {
			character.SetActive (true);
		}
	}

	private void reactivateAllControls() {
		foreach (GameObject control in controls) {
			control.SetActive (true);
		}
	}

	public void closeMenu() {
		reactivateAllCharacters ();
		reactivateAllControls();
		messageMenu.SetActive (false);
	}

	public void StartGame() {
		mainMenu.SetActive (false);
		messageMenu.SetActive (true);
		messageMenu.transform.position = player.transform.position + menuCameraBuffer;
		gameServer.StartGame ();
		player.transform.position = startingPoint.transform.position;
	}

}
