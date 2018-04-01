using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    public float knockback_force = 1f;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Knock(Vector3 direction)
    {
        Debug.Log("Knocking back " + gameObject.name);
		rb.AddForce (knockback_force * direction, ForceMode.Impulse);
    }
}
