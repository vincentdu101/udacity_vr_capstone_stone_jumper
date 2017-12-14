using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoiceService : MonoBehaviour {

	private GameDataModel.CharacterChoice choice;

	public void SetChoice(GameDataModel.CharacterChoice choice) {
		this.choice = choice;
	}

	public GameDataModel.CharacterChoice GetChoice() {
		return choice;
	}
}
