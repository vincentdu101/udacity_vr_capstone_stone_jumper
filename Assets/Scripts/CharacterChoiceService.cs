using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContactService : MonoBehaviour {

	private GameDataModel.Contact contact;

	public void SetContact(GameDataModel.Contact contact) {
		this.contact = contact;
	}

	public GameDataModel.Contact GetContact() {
		return contact;
	}
}
