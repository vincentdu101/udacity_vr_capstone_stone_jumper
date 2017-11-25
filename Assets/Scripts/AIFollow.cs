﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour {

	GameObject[] goalLocations;
	NavMeshAgent agent;
	Animator anim;

	// Use this for initialization
	void Start () {
		goalLocations = GameObject.FindGameObjectsWithTag("Goal");
		agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.SetDestination(goalLocations[Random.Range(0,goalLocations.Length)].transform.position);
		agent.speed = Random.Range (1, 2);
		agent.acceleration = Random.Range (1, 8);

		//anim = this.GetComponent<Animator> ();
		//anim.SetTrigger ("isWalking");
	}

	// Update is called once per frame
	void Update () {
		if (agent.remainingDistance < 1) {
			agent.SetDestination (goalLocations [Random.Range (0, goalLocations.Length)].transform.position);
		}
	}
}
