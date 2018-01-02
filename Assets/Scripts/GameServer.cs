using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServer : Photon.PunBehaviour {

	public GameObject gameStateObj;
	private GameState gameState;
	private string gameVersion = "1";

	// Use this for initialization
	void Start () {
		gameState = gameStateObj.GetComponent<GameState> ();
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (PhotonNetwork.connectionStateDetailed);
		//Connect ();
	}

	void Awake() {
		//PhotonNetwork.autoJoinLobby = false;
		//PhotonNetwork.automaticallySyncScene = true;
	}

	public void Connect() {
		if (!PhotonNetwork.connected) {
			PhotonNetwork.ConnectUsingSettings (gameVersion);
		}
	}

	public void StartGame() {
		gameState.StartGame ();
		//RoomOptions roomOptions = new RoomOptions ();
		//roomOptions.maxPlayers = 4;
		//PhotonNetwork.JoinOrCreateRoom ("viking-lands", roomOptions, null);
	}

	public void OnJoinedRoom() {
		Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
		//PhotonNetwork.Instantiate (player.name, player.transform.position, player.transform.rotation, 0);
	}
}
