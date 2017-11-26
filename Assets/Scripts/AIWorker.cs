using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWorker : MonoBehaviour {

	GameObject[] goalLocations;
	NavMeshAgent agent;
	Animation animation;

	// Use this for initialization
	void Start () {
		SetupWorkerAndGoal ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckForTransitionToLumbering ();
	}

	private void SetupWorkerAndGoal() {
		goalLocations = GameObject.FindGameObjectsWithTag ("WorkerGoal");
		animation = this.GetComponent<Animation> ();
		agent = this.GetComponent<NavMeshAgent> ();
		agent.SetDestination (goalLocations [Random.Range (0, goalLocations.Length)].transform.position);
		agent.speed = Random.Range (1, 2);
		agent.acceleration = Random.Range (1, 8);
	}

	private void CheckForTransitionToLumbering() {
		if (agent.remainingDistance < 1) {
			agent.SetDestination (agent.transform.position);
			animation.Play ("Lumbering");
		}
	}
}
