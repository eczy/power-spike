using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falloff : MonoBehaviour {

	public float falloff_y_value = 0f;
	public float respawn_time = 0f;
	public Transform[] battery_respawn;
	public Transform player_spawn;

	Vector3 original_position;
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= falloff_y_value)
        {
			StartCoroutine (Respawn ());

            PlayerToStats stats = GetComponent<PlayerToStats>();
            stats.ReportDeath();
        }
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

		r.enabled = false;
		m.enabled = false;
		a.enabled = false;
		h.enabled = false;
		foreach (Renderer rend in GetComponentsInChildren<Renderer>())
			rend.enabled = false;

		transform.position = player_spawn.position;
		transform.rotation = player_spawn.rotation;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		yield return new WaitForSeconds (respawn_time);

		r.enabled = true;
		m.enabled = true;
		a.enabled = true;
		h.enabled = true;
		foreach (Renderer rend in GetComponentsInChildren<Renderer>())
			rend.enabled = true;
		transform.forward = player_spawn.forward;
        if (health != null)
		    health.health = health.max_health;
	}

    Vector3 GetClosestBatterySpawn(Vector3 position)
    {
        Vector3 nearest = Vector3.zero;
        float min_dist = Mathf.Infinity;
        for (int i = 0; i < battery_respawn.Length; i++)
        {
            float dist = Vector3.Distance(position, battery_respawn[i].position);

            if (dist < min_dist)
            {
                min_dist = dist;
                nearest = battery_respawn[i].position;
            }
        }
        return nearest;
    }
}
