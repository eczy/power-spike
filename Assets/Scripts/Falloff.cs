using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falloff : MonoBehaviour {
	public float respawnTime;
	public Transform[] batteryRespawn;
	public Transform playerSpawn;

	private Vector3 originalPosition;
	
	private void Update () {
		CheckBounds();
	}

	private void CheckBounds()
	{
		if (!BoundryManager.OutOfBounds(transform.position)) return;
		
		StartCoroutine (Respawn());

		PlayerToStats stats = GetComponent<PlayerToStats>();
		stats.ReportDeath();
	}

	IEnumerator Respawn(){
		Battery bat = GetComponent<BatteryCollector> ().GetBattery ();
		if (bat != null) {
			GetComponent<BatteryCollector> ().RemoveBattery ();
			bat.transform.position = GetClosestBatterySpawn(bat.transform.position);
		}

		Renderer r = GetComponent<Renderer> ();
		PlayerMovement m = GetComponent<PlayerMovement> ();
		PlayerAttack a = GetComponent<PlayerAttack> ();
		Hitbox h = GetComponent<Hitbox> ();
		Health health = GetComponent<Health> ();
        Rigidbody rb = GetComponent<Rigidbody>();
        Collider coll = GetComponent<Collider>();

		r.enabled = false;
		m.enabled = false;
		a.enabled = false;
		h.enabled = false;
        coll.enabled = false;
        rb.isKinematic = true;
		foreach (Renderer rend in GetComponentsInChildren<Renderer>())
			rend.enabled = false;

		transform.position = playerSpawn.position;
		transform.rotation = playerSpawn.rotation;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		yield return new WaitForSeconds (respawnTime);

		r.enabled = true;
		m.enabled = true;
		a.enabled = true;
		h.enabled = true;
        coll.enabled = true;
        rb.isKinematic = false;
		foreach (Renderer rend in GetComponentsInChildren<Renderer>())
			rend.enabled = true;
		transform.forward = playerSpawn.forward;
        if (health != null)
		    health.health = health.max_health;
	}

	private Vector3 GetClosestBatterySpawn(Vector3 position)
    {
        Vector3 nearest = Vector3.zero;
        float minDist = Mathf.Infinity;
	    
        foreach (Transform t in batteryRespawn)
        {
	        float dist = Vector3.Distance(position, t.position);

	        if (dist < minDist)
	        {
		        minDist = dist;
		        nearest = t.position;
	        }
        }
	    
        return nearest;
    }
}
