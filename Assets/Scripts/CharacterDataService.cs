using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterDataService : MonoBehaviour {

	private string gameDataFileName = "characters.json";
	private GameObject gameData;
	private InventoryDataService inventoryDataService;
	private int stage = 1;
	private Dictionary<string, GameDataModel.CharacterChoice> keyFigureProgress;
	public string[] names;
	public Dictionary<string, GameDataModel.CharacterChoice> previousChoices;
	public Dictionary<string, GameDataModel.CharacterChoice> choices;

	// Use this for initialization
	void Start () {
		choices = new Dictionary<string, GameDataModel.CharacterChoice> ();
		previousChoices = new Dictionary<string, GameDataModel.CharacterChoice> ();
		gameData = GameObject.FindGameObjectWithTag ("GameData");
		inventoryDataService = gameData.GetComponent<InventoryDataService> ();
		LoadDataFromFile ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void LoadChoicesIntoDictionary(GameDataModel.CharacterChoice[] inputChoices) {
		foreach (GameDataModel.CharacterChoice choice in inputChoices) {
			if (!choices.ContainsKey (choice.id)) {
				Debug.Log (choice.id);
				Debug.Log (choice);
				choices.Add (choice.id, choice);
			}
		}
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
			LoadChoicesIntoDictionary (data.choices);
		} else {
			Debug.LogError("Cannot load game data!");
		}
	}

	public GameDataModel.CharacterChoice GetRandomChoice() {
		int randomChoice = Random.Range (0, 2);
		string key = "VC" + stage + "S" + randomChoice + "-0";
		GameDataModel.CharacterChoice character;
		choices.TryGetValue(key, out character);
		return character;
	}

	public GameDataModel.CharacterChoice GetNextPositiveChoice(GameDataModel.CharacterChoice choice) {
		GameDataModel.CharacterChoice character;
		choices.TryGetValue (choice.nextPositiveSequence, out character);
		if (character.resetChoice != null) {
			UpdateResetChoice (character);
		}
		if (choice.removeItem != null) {
			inventoryDataService.RemoveItem (choice.removeItem);
		}
		return character;
	}

	public GameDataModel.CharacterChoice GetNextNegativeChoice(GameDataModel.CharacterChoice choice) {
		GameDataModel.CharacterChoice character;
		choices.TryGetValue (choice.nextNegativeSequence, out character);
		if (character.resetChoice != null) {
			UpdateResetChoice (character);
		}
		if (choice.removeItem != null) {
			inventoryDataService.RemoveItem (choice.removeItem);
		}
		return character;
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

	public GameDataModel.CharacterChoice GetKeyFigureChoice(string figure) {
		GameDataModel.CharacterChoice previousChoice;
		GameDataModel.CharacterChoice nextChoice;
		string key; 
		previousChoices.TryGetValue(figure, out previousChoice);
		if (previousChoice == null) {
			key = "VC" + stage + "S0" + "-0" + GetKeyFigureSequence(figure);
		} else {
			key = previousChoice.resetChoice;
		}
		choices.TryGetValue(key, out nextChoice);
		previousChoices.Remove (figure);
		previousChoices.Add (figure, nextChoice);
		return nextChoice;
	}

	public string GetCharacterName() {
		int random = Random.Range (0, names.Length - 1);
		return names[random];
	}

	public void UpdateResetChoice(GameDataModel.CharacterChoice choice) {
		GameDataModel.CharacterChoice previousChoice;
		string key; 
		previousChoices.TryGetValue(choice.character, out previousChoice);
		if (previousChoice == null) {
			key = "VC" + stage + "S0" + "-0" + GetKeyFigureSequence(choice.character);
		} else {
			key = previousChoice.resetChoice;
		}
		previousChoices.Remove (choice.character);
		previousChoices.Add (choice.character, choice);
	}
}
