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
		public string resetChoice;
		public string itemGranted;
		public string positiveRequirement;
		public string character;
		public string positiveItemGone;
		public string removeItem;
		public void clone(CharacterChoice choice) {
			this.id = choice.id;
			this.text = choice.text;
			this.btns = choice.btns;
			this.positiveText = choice.positiveText;
			this.negativeText = choice.negativeText;
			this.close = choice.close;
			this.nextPositiveSequence = choice.nextPositiveSequence;
			this.nextNegativeSequence = choice.nextNegativeSequence;
		}
	}

}

