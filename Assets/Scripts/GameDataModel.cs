using UnityEngine;
using System.Collections;

public class GameDataModel : MonoBehaviour
{

	[System.Serializable]
	public class GameData {
		public string[] names;
		public CharacterChoice[] choices;
	}

	[System.Serializable]
	public class CharacterChoice {
		public string id;
		public string text;
		public string[] btns;
		public string positiveText;
		public string negativeText;
		public string close;
		public string nextPositiveSequence;
		public string nextNegativeSequence;
	}

}

