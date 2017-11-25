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

	// Use this for initialization
	void Start () {
		if (gameController) {
			gameServer = gameController.GetComponent<GameServer> ();
		}
		characters = GameObject.FindGameObjectsWithTag ("Character");
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void reactivateAllCharacters() {
		foreach (GameObject character in characters) {
			character.SetActive (true);
		}
	}

	public void closeMenu() {
		reactivateAllCharacters ();
		messageMenu.SetActive (false);
	}

	public void StartGame() {
		mainMenu.SetActive (false);
		messageMenu.SetActive (true);
		messageMenu.transform.position = player.transform.position + new Vector3(0.0f, 0.5f, 3.25f);
		gameServer.StartGame ();
		player.transform.position = startingPoint.transform.position;
	}

}
