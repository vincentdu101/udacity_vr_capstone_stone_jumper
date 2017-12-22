using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TraitorAI : MonoBehaviour {

	GameObject[] goalLocations;
	NavMeshAgent agent;
	Animator anim;
	Animation animations;
	GameObject player;
	float chaseDistance = 5.0f;
	float attackDistance = 1.0f;

	// Use this for initialization
	void Start () {
		goalLocations = GameObject.FindGameObjectsWithTag("TraitorGoal");
		agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.SetDestination(goalLocations[Random.Range(0,goalLocations.Length)].transform.position);
		agent.speed = Random.Range (1, 2);
		agent.acceleration = Random.Range (1, 8);
		animations = agent.GetComponent<Animation>();
		animations.PlayQueued ("Walk");
		player = GameObject.FindGameObjectWithTag ("Player");

		//anim = this.GetComponent<Animator> ();
		//anim.SetTrigger ("isWalking");
	}

	// Update is called once per frame
	void Update () {
		if (agent.remainingDistance < 1) {
			agent.SetDestination (goalLocations [Random.Range (0, goalLocations.Length)].transform.position);
		}
		DeterminePlayerContact ();
	}

	private void DeterminePlayerContact() {
		Vector3 playerDirection = player.transform.position - this.transform.position;

		if (playerDirection.magnitude < chaseDistance) {
			agent.SetDestination (player.transform.position);
		} else if (playerDirection.magnitude < attackDistance) {
			animations.PlayQueued ("Lumbering");
		} else {
			animations.PlayQueued ("Walk");
		}
	}
}
