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
		goalLocations = GameObject.FindGameObjectsWithTag ("WorkerGoal");
		animation = this.GetComponent<Animation> ();
		agent = this.GetComponent<NavMeshAgent> ();
		agent.SetDestination (goalLocations [Random.Range (0, goalLocations.Length)].transform.position);
		agent.speed = Random.Range (1, 2);
		agent.acceleration = Random.Range (1, 8);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("remaining distance " + agent.remainingDistance);
		if (agent.remainingDistance < 1) {
			Debug.Log ("playing lumbering");
			animation.PlayQueued ("Lumbering");
		}
	}
}
