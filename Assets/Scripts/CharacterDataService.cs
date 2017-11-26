using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterDataService : MonoBehaviour {

	private string gameDataFileName = "characters.json";
	private int stage = 1;
	private int sequence = 0;
	public string[] names;
	public Dictionary<string, GameDataModel.CharacterChoice> choices;

	// Use this for initialization
	void Start () {
		choices = new Dictionary<string, GameDataModel.CharacterChoice> ();
		LoadDataFromFile ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void LoadChoicesIntoDictionary(GameDataModel.CharacterChoice[] inputChoices) {
		foreach (GameDataModel.CharacterChoice choice in inputChoices) {
			if (!choices.ContainsKey (choice.id)) {
				Debug.Log (choice.id);
				choices.Add (choice.id, choice);
			}
		}
	}

	private void LoadDataFromFile() {
		// Path.Combine combines strings into a file path
		// Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
		Debug.Log(Application.streamingAssetsPath);
		string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

		if(File.Exists(filePath)) {
			// Read the json from the file into a string
			string dataAsJson = File.ReadAllText(filePath); 
			// Pass the json to JsonUtility, and tell it to create a GameData object from it
			GameDataModel.GameData data = JsonUtility.FromJson<GameDataModel.GameData>(dataAsJson);

			// Retrieve the names and choices property of data
			names = data.names;
			Debug.Log (names);
			LoadChoicesIntoDictionary (data.choices);
		} else {
			Debug.LogError("Cannot load game data!");
		}
	}

	public GameDataModel.CharacterChoice GetRandomChoice() {
		int randomChoice = Random.Range (0, 2);
		string key = "VC" + stage + "S" + randomChoice + "-" + sequence;
		return choices [key];
	}

}
