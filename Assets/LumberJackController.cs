using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberJackController : MonoBehaviour {

	Animator anim;
	bool reachedGoal = false;

	void OnTriggerEnter(Collider obj) {
		Debug.Log ("i have collided");
		Debug.Log (obj);
		anim.SetBool ("isChopping", true);
	}

	// Use this for initialization
	void Start () {
		anim = this.transform.GetComponent<Animator> ();
		anim.SetBool ("isWalking", true);
	}
	
	// Update is called once per frame
	void Update () {
		if (!reachedGoal) {
			anim.SetBool ("isWalking", true);
		}
	}
}
