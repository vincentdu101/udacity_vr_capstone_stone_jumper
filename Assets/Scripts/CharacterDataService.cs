using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterDataService : MonoBehaviour {

	private string gameDataFileName = "characters.json";
	private GameObject gameData;
	private GameObject gameStateObj;
	private GameState gameState;
	private InventoryDataService inventoryDataService;
	private string serverPath = "http://localhost:3000/graphql";
	private int stage = 1;
	private int[] soldierContacts = {1, 4, 5};
	private Dictionary<string, GameDataModel.Contact> keyFigureProgress;
	public string[] names;
	public Dictionary<string, GameDataModel.Contact> previousContacts;
	public Dictionary<string, GameDataModel.Contact> contacts;

	// Use this for initialization
	void Start () {
		contacts = new Dictionary<string, GameDataModel.Contact> ();
		previousContacts = new Dictionary<string, GameDataModel.Contact> ();
		gameData = GameObject.FindGameObjectWithTag ("GameData");
		inventoryDataService = gameData.GetComponent<InventoryDataService> ();
		gameStateObj = GameObject.FindGameObjectWithTag ("GameState");
		gameState = gameStateObj.GetComponent<GameState> ();
		
		// send data from server and local name file
		LoadDataFromServer(POST());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public WWW POST() {
		WWW www;
		Hashtable postHeader = new Hashtable();
		postHeader.Add("Content-Type", "application/json");

		// convert json string to byte
		var formData = System.Text.Encoding.UTF8.GetBytes("{ \"query\": \"{" + 
			"game(id: 1) {" +
				"id " +
				"title " + 
				"contacts { " + 
					"id " + 
					"text " +
					"hierarchy " +
					"characterType " +
					"itemGranted " + 
					"itemGone " + 
					"finishTask " +
					"requirement " +
					"choices { " +
						"id " +
						"text " +
						"itemGranted " +
						"itemGone " +
						"finishTask " +
						"requirement " +
						"choiceType " +
						"contacts {id} " +
					"} " +
				"}" +
			"}" +
		"}\" }");

		www = new WWW(this.serverPath, formData, postHeader);
		StartCoroutine(LoadDataFromServer(www));
		return www;
	}

	private void LoadContactsIntoDictionary(GameDataModel.Contact[] inputContacts) {
		// add contact to list of contacts if not already in the list
		foreach (GameDataModel.Contact contact in inputContacts) {
			string contactHeirarchyKey = this.GenerateContactKey(contact.id, contact.hierarchy, contact.characterType);
			if (!contacts.ContainsKey (contactHeirarchyKey)) {
				contacts.Add(contactHeirarchyKey, contact);
			}

			if (!previousContacts.ContainsKey(contact.characterType)) {
				previousContacts.Add(contact.characterType, contact);
			}
		}
	}

	private string GenerateContactKey(int contactId, int hierarchy, string characterType) {
		return contactId + "-" + hierarchy + "-" + this.GetKeyFigureSequence(characterType);
	}

	private void LoadDataFromFile() {
		// Path.Combine combines strings into a file path
		// Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
		string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

		if(File.Exists(filePath)) {
			// Read the json from the file into a string
			string dataAsJson = File.ReadAllText(filePath); 
			// Pass the json to JsonUtility, and tell it to create a GameData object from it
			GameDataModel.GameData data = JsonUtility.FromJson<GameDataModel.GameData>(dataAsJson);

			// Retrieve the names and choices property of data
			names = data.names;
			// LoadChoicesIntoDictionary (data.choices);
		} else {
			Debug.LogError("Cannot load game data!");
			filePath = Path.Combine("jar:file://" + Application.dataPath + "!/assets/", gameDataFileName);
			StartCoroutine (GetDataInAndroid (filePath));
		}
	}

	private IEnumerator LoadDataFromServer(WWW data) {
		yield return data; // Wait until the download is done
		if (data.error != null)
		{
			Debug.Log("There was an error sending request: " + data.error);
		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
			LoadDataFromFile();
			GameDataModel.GameData parsedData = JsonUtility.FromJson<GameDataModel.GameData>(data.text);
			LoadContactsIntoDictionary(parsedData.data.game.contacts);
		}
	}

	private IEnumerator GetDataInAndroid(string url) {
		WWW www = new WWW(url);
		yield return www;
		if (www.text != null) {

			string dataAsJson = www.text;
			GameDataModel.GameData data = JsonUtility.FromJson<GameDataModel.GameData>(dataAsJson);

			// Retrieve the names and choices property of data
			names = data.names;
		} else {
			Debug.LogError ("Cannot load game data!");
		}
	}

	private string FindNextChoiceContactKey(GameDataModel.Contact contact, GameDataModel.Choice choice) {
		int nextContactId = 0;
		
		foreach (GameDataModel.ContactId contactId in choice.contacts) {
			if (contact.id != contactId.id) {
				nextContactId = contactId.id;
				break;
			}
		}
		return this.GenerateContactKey(nextContactId, stage, contact.characterType);
	}

	public GameDataModel.Contact GetRandomContact() {
		int randomChoice = soldierContacts[Random.Range (0, 2)];
		string key = this.GenerateContactKey(randomChoice, stage, "SOLDIER");
		GameDataModel.Contact character;
		contacts.TryGetValue(key, out character);
		return character;
	}

	public GameDataModel.Contact GetNextChoice(GameDataModel.Contact contact, int choice) {
		GameDataModel.Contact nextContact;
		GameDataModel.Choice nextChoice = contact.choices[choice];
		contacts.TryGetValue (this.FindNextChoiceContactKey(contact, nextChoice), out nextContact);
		if (nextContact == null) {
			return null;
		}

		UpdateResetChoice (nextContact);
		if (nextChoice.itemGone != null) {
			inventoryDataService.RemoveItem (nextChoice.itemGone);
		}
		if (nextContact.finishTask != null) {	
			gameState.FinishTask (nextContact.finishTask);
		}
		return nextContact;
	}

	public string GetKeyFigureSequence(string figure) {
		switch (figure) {
		case "EricTheRed": {
			return "A";
		}
		case "Illugi": {
				return "B";
		}
		case "ShipCaptain": {
				return "C";
		}
		case "Glaumur": {
				return "D";
		}
		default:	
			return "0";
		}
	}

	public GameDataModel.Contact GetKeyFigureChoice(string figure) {
	// 	GameDataModel.Contact previousContact;
	// 	GameDataModel.Contact nextContact;
	// 	string key; 
	// 	previousContacts.TryGetValue(figure, out previousContact);
	// 	if (previousContact == null) {
	// 		key = "VC" + stage + "S0" + "-0" + GetKeyFigureSequence(figure);
	// 	} else {
	// 		key = previousChoice.resetChoice;
	// 	}
	// 	contacts.TryGetValue(key, out nextContact);
	// 	previousContacts.Remove (figure);
	// 	previousContacts.Add (figure, nextChoice);
	// 	return nextChoice;
		GameDataModel.Contact nextContact;
		previousContacts.TryGetValue(figure, out nextContact);
		return nextContact;
	}

	public string GetCharacterName() {
		int random = Random.Range (0, names.Length - 1);
		return names[random];
	}

	public void UpdateResetChoice(GameDataModel.Contact contact) {
		previousContacts.Remove (contact.characterType);
		previousContacts.Add (contact.characterType, contact);
	}
}
