using UnityEngine;
using System.Collections;

public class GameDataModel : MonoBehaviour
{

	[System.Serializable]
	public class GameData {
		public string[] names;
		public Data data;
	}

	[System.Serializable]
	public class Data {
		public Game game;
	}

	[System.Serializable]
	public class Game {
		public int id;
		public string title;
		public Contact[] contacts;
	}

	[System.Serializable]
	public class Contact {
		public int id;
		public string text;
		public string characterType;
		public int hierarchy; 
		public string itemGranted;
		public string requirement;
		public string itemGone;
		public string finishTask;

		public Choice[] choices;

		public void clone(Contact contact) {
			this.id = contact.id;
			this.text = contact.text;
			this.characterType = contact.characterType;
			this.hierarchy = contact.hierarchy;
			this.choices = contact.choices;
			this.requirement = contact.requirement;
			this.itemGranted = contact.itemGranted;
			this.itemGone = contact.itemGone;
			this.finishTask = contact.finishTask;
		}
	}

	[System.Serializable]
	public class ContactId {
		public int id;
	}

	[System.Serializable]
	public class Choice {
		public int id;
		public string text;
		public string choiceType;
		public string itemGranted;
		public string requirement;
		public string itemGone;
		public string finishTask;
		public ContactId[] contacts;

		public void clone(Choice choice) {
			this.id = choice.id;
			this.text = choice.text;
			this.choiceType = choice.choiceType;
			this.requirement = choice.requirement;
			this.itemGranted = choice.itemGranted;
			this.itemGone = choice.itemGone;
			this.finishTask = choice.finishTask;
			this.contacts = choice.contacts;
		}
		public void RemoveDupContactId(int inContactId) {
			ContactId[] newContacts = new ContactId[contacts.Length];
			for(int x = 0; x < this.contacts.Length; x++) {
				ContactId contactId = this.contacts[x];
				if (contactId.id != inContactId) {
					newContacts[x] = contactId;
				}
			}
			this.contacts = newContacts;
		}
	}

}

