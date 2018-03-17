using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

	public float cooldown = 2;

	bool onCooldown = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    void StartCooldown() {
		StartCoroutine (CooldownRoutine());
	}

	IEnumerator CooldownRoutine() {
		onCooldown = true;
		yield return new WaitForSeconds (cooldown);
		onCooldown = false;
	}

	public void SetOwner(GameObject owner) {
		if (onCooldown)
			return;
		
		transform.parent = owner.transform;
		transform.localPosition = Vector3.up * 2;
		StartCooldown ();
	}
}
