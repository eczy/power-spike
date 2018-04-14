using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryAnimatorInput : MonoBehaviour {

    Rigidbody rb;
    Animator anim;
    PlayerMovement movement;

	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
	}
	
	void Update () {
        anim.SetFloat("yVelocity", rb.velocity.y / movement.maxFallSpeed);
        anim.SetBool("grounded", movement.IsGrounded());
	}
}
