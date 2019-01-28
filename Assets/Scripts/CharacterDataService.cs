using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GameDataModel;

public class CharacterDataService : MonoBehaviour {

	private string gameDataFileName = "characters.json";
	private GameObject gameData;
	private GameObject gameStateObj;
	private GameState gameState;
	private InventoryDataService inventoryDataService;
	private string serverPath = "http://localhost:3000/graphql";
	private int stage = 1;
	private int[] soldierContacts = {1, 4, 5};
	private Dictionary<string, Contact> keyFigureProgress;
	public string[] names;
	public Dictionary<string, Contact> previousContacts;
	public Dictionary<string, int> keyFigureHierarchyCount;
	public Dictionary<string, Contact> contacts;

	// Use this for initialization
	void Start () {
		contacts = new Dictionary<string, Contact> ();
		previousContacts = new Dictionary<string, Contact> ();
		keyFigureHierarchyCount = new Dictionary<string, int>();
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

	private void LoadContactsIntoDictionary(Contact[] inputContacts) {
		// add contact to list of contacts if not already in the list
		foreach (Contact contact in inputContacts) {
			string figure = contact.characterType;
			int defaultHierarchy = 1;
			if (!keyFigureHierarchyCount.ContainsKey(figure) && IsKeyFigure(figure)) {
				keyFigureHierarchyCount.Add(figure, defaultHierarchy);
			}

			string contactHeirarchyKey = this.GenerateContactKey(contact.id, contact.characterType);
			if (!contacts.ContainsKey (contactHeirarchyKey)) {
				contacts.Add(contactHeirarchyKey, contact);
			}

			if (!previousContacts.ContainsKey(figure)) {
				previousContacts.Add(figure, contact);
			}
		}
	}

	private string GenerateContactKey(int contactId, string characterType) {
		int hierarchy;
		keyFigureHierarchyCount.TryGetValue(characterType, out hierarchy);
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
			GameData data = JsonUtility.FromJson<GameData>(dataAsJson);

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
			GameData parsedData = JsonUtility.FromJson<GameData>(data.text);
			LoadContactsIntoDictionary(parsedData.data.game.contacts);
		}
	}

	private IEnumerator GetDataInAndroid(string url) {
		WWW www = new WWW(url);
		yield return www;
		if (www.text != null) {

			string dataAsJson = www.text;
			GameData data = JsonUtility.FromJson<GameData>(dataAsJson);

			// Retrieve the names and choices property of data
			names = data.names;
		} else {
			Debug.LogError ("Cannot load game data!");
		}
	}

	private void UpdateHeirarchyOfKeyFigure(Contact contact, Choice choice) {
		if (choice.choiceType == "NORMAL") {
			int hierarchy;
			keyFigureHierarchyCount.TryGetValue(contact.characterType, out hierarchy);
			keyFigureHierarchyCount.Remove(contact.characterType);
			keyFigureHierarchyCount.Add(contact.characterType, hierarchy++);
		}
	}

	private string FindNextChoiceContactKey(Contact contact, Choice choice) {
		int nextContactId = 0;
		
		foreach (ContactId contactId in choice.contacts) {
			if (contact.id != contactId.id) {
				nextContactId = contactId.id;
				break;
			}
		}
		UpdateHeirarchyOfKeyFigure(contact, choice);
		return this.GenerateContactKey(nextContactId, contact.characterType);
	}

	private Contact RemoveCurrentChoiceInNextContact(Contact contact, Choice choice) {
		Choice[] validChoices = new Choice[contact.choices.Length - 1];
		int nextSlot = 0;
		for (int x = 0; x < contact.choices.Length; x++) {
			if (contact.choices[x].id != choice.id) {
				validChoices[nextSlot] = contact.choices[x];
				nextSlot++;
			}
		}
		contact.choices = validChoices;
		return contact;
	}
	private bool IsKeyFigure(string figure) {
		return GetKeyFigureSequence(figure) != "0";
	}

	public Contact GetRandomContact() {
		int randomChoice = soldierContacts[Random.Range (0, 2)];
		string key = this.GenerateContactKey(randomChoice, "SOLDIER");
		Contact character;
		contacts.TryGetValue(key, out character);
		return character;
	}

	public Contact GetNextChoice(Contact contact, int choice) {
		Contact nextContact;
		Choice nextChoice = contact.choices[choice];
		Debug.Log(nextChoice.id);
		contacts.TryGetValue (this.FindNextChoiceContactKey(contact, nextChoice), out nextContact);
		if (nextContact == null) {
			return null;
		}

		UpdateResetChoice (nextContact);
		nextContact = RemoveCurrentChoiceInNextContact(nextContact, nextChoice);
		Debug.Log(nextChoice.id);
		Debug.Log(nextChoice.itemGone == "");
		if (nextChoice.itemGone != "") {
			inventoryDataService.RemoveItem (nextChoice.itemGone);
		}
		if (nextContact.finishTask != "") {	
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

	public Contact GetKeyFigureChoice(string figure) {
	// 	Contact previousContact;
	// 	Contact nextContact;
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
		Contact nextContact;
		previousContacts.TryGetValue(figure, out nextContact);
		return nextContact;
	}

	public string GetCharacterName() {
		int random = Random.Range (0, names.Length - 1);
		return names[random];
	}

	public void UpdateResetChoice(Contact contact) {
		previousContacts.Remove (contact.characterType);
		previousContacts.Add (contact.characterType, contact);
	}
}
