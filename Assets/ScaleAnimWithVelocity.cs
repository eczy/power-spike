using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimWithVelocity : MonoBehaviour {

    public Animator[] animators;
    public float multiplier = 1f;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        foreach (Animator a in animators)
            a.speed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Animator a in animators)
            a.speed = rb.velocity.magnitude * multiplier;
	}
}
