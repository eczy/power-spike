using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

	public float nudge_force = 1f;
	public float nudge_torque = 1f;
	public float y_delta = 1f;

	Health health;
	RigidbodyConstraints constraints;
	Rigidbody rb;
	PlayerMovement move;
	PlayerAttack attack;
	[HideInInspector] public bool dead = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		constraints = rb.constraints;
		health = GetComponent<Health> ();
		move = GetComponent<PlayerMovement> ();
		attack = GetComponent<PlayerAttack> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (health.health == 0 && !dead)
			StartCoroutine (Die ());
		if (health.health != 0 && dead)
			StartCoroutine (Reset ());
	}

	IEnumerator Die(){
		dead = true;
		rb.constraints = RigidbodyConstraints.None;
		move.enabled = false;
		attack.enabled = false;
		rb.AddForceAtPosition (Vector3.forward * -1 * nudge_force, new Vector3(transform.position.x, transform.position.y + y_delta, transform.position.z), ForceMode.Impulse);
		rb.AddTorque (transform.up * nudge_torque, ForceMode.Impulse);
		yield return null;
	}

	IEnumerator Reset(){
		dead = false;
		rb.constraints = constraints;
		yield return null;
	}


}
