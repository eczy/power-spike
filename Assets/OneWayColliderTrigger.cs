using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OneWayColliderTrigger : MonoBehaviour {
	public Collider platform;

	Collider c;
	// Use this for initialization
	void Start () {
		c = GetComponent<Collider> ();
		platform.isTrigger = false;
	}

	void OnTriggerEnter(Collider co){
		Physics.IgnoreCollision (platform, co, true);
	}

	void OnTriggerExit (Collider co){
		Physics.IgnoreCollision (platform, co, false);
	}
}
