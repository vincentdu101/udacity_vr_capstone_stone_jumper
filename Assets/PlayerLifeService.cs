using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeService : MonoBehaviour {

	private double defaultLifeCount = 100.0;
	private double currentLife = 0.0;
	private double recoverRate = 0.001;
	private bool damageTaken = false;
	private GameObject playerLifeText;

	void Start() {
		currentLife = defaultLifeCount;
		playerLifeText = GameObject.FindGameObjectWithTag ("Life");
	}

	void Update() {
		if (currentLife < defaultLifeCount && currentLife != 0.0) {
			currentLife += recoverRate;
		}
		playerLifeText.GetComponent<Text> ().text = currentLife + " / " + defaultLifeCount;
	}

	public void lifeTakingHit() {
		currentLife -= 1.0;
		damageTaken = true;
	}

	public void resetLife() {
		currentLife = 100.0;
	}

	public double getLife() {
		return currentLife;
	}


}
