using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerLifeService : MonoBehaviour {

	private double defaultLifeCount = 100.0;
	private double currentLife = 0.0;
	private double recoverRate = 0.01;
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
		playerLifeText.GetComponent<Text> ().text = OutputLifeInfo ();
	}

	private string OutputLifeInfo() {
		string formatCurrent = currentLife.ToString("F");
		return "Life: " + formatCurrent + " / " + defaultLifeCount;
	}

	public void lifeTakingHit() {
		currentLife -= 0.03;
		damageTaken = true;
	}

	public void resetLife() {
		currentLife = 100.0;
	}

	public double getLife() {
		return currentLife;
	}


}
