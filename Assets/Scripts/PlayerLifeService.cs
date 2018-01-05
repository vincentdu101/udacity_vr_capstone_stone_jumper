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
	private GameObject damageFilter;
	private GameObject player;

	void Start() {
		currentLife = defaultLifeCount;
		player = GameObject.FindGameObjectWithTag ("Player");
		damageFilter = GameObject.Find ("DamageFilter");
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
		damageFilter.transform.position = player.transform.position;
		damageFilter.SetActive (true);
		currentLife -= 0.2;
		damageTaken = true;
		damageFilter.transform.position = damageFilter.transform.position + new Vector3(0.0f, 1000.0f, 0.0f);
	}

	public void resetLife() {
		currentLife = 100.0;
		playerLifeText.GetComponent<Text> ().text = OutputLifeInfo ();
	}

	public double getLife() {
		return currentLife;
	}


}
