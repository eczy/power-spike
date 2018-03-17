using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    public float knockback_force = 1f;
    public float explosion_radius = 1f;
    public float upwards_modifier = 1f;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Knock(Vector3 position)
    {
        rb.AddExplosionForce(knockback_force, position, explosion_radius, upwards_modifier, ForceMode.Impulse);
    }
}
