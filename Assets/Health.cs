using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public float max_health = 1f;
	public float health = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddHealth(float amount){
		health += amount;
		if (health > max_health)
			health = max_health;
	}

	public void Hurt(float amount){
		health -= amount;
		if (health < 0)
			health = 0;
	}
}
