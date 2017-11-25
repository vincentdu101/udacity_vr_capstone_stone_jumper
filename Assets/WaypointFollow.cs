using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour {

	public GameObject[] waypoints;
	int currentWP = 0;

	float speed = 1.0f;
	float accuracy = 0.1f;
	float rotSpeed = 1.0f;

	bool stopMovement = false;

	// Use this for initialization
	void Start () {
		waypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
	}

	// Update is called once per frame
	void Update () {
		if (waypoints.Length == 0 || stopMovement) { 
			return;
		}

		Vector3 lookAtGoal = new Vector3 (waypoints [currentWP].transform.position.x,
			this.transform.position.y, waypoints [currentWP].transform.position.z);
		Vector3 direction = lookAtGoal - this.transform.position;
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation,
			Quaternion.LookRotation (direction), Time.deltaTime * rotSpeed);

		// check distance between object and target to see if we have reached the target
		if (direction.magnitude < accuracy) {
			currentWP++;

			// cycles through all waypoints until we exhaust the waypoints, then we go back to the beginning
			if (currentWP >= waypoints.Length) {
				currentWP = 0;
			}
		}
		this.transform.Translate (0, 0, speed * Time.deltaTime);
	}

	public void StopMovement() {
		stopMovement = true;	
	}
}
