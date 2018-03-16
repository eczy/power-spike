using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : MonoBehaviour {

	Quaternion rot;

	// Use this for initialization
	void Awake () {
		rot = transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.rotation = rot;
	}
}
