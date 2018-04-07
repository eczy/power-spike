using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falloff : MonoBehaviour {
	public float respawnTime;
	public Transform[] batteryRespawn;
	public Transform playerSpawn;

	private Vector3 originalPosition;
    private PlayerMovement m;
    private PlayerAttack a;
    private Hitbox h;
    private Rigidbody rb;
    private Collider coll;
	private bool isRespawning;

	private void Start()
	{
		isRespawning = false;
		coll = GetComponent<Collider>();
		rb = GetComponent<Rigidbody>();
		h = GetComponent<Hitbox>();
		a = GetComponent<PlayerAttack>();
		m = GetComponent<PlayerMovement>();
	}

	private void Update () {
		CheckBounds();
	}

	private void CheckBounds()
	{
		if (!BoundryManager.OutOfBounds(transform.position)) return;

		if (BoundryManager.GetOutOfBoundsDirection(transform.position) == Vector3.up)
		{
			// Hit camera
		}
		StartCoroutine(Respawn());

		PlayerToStats stats = GetComponent<PlayerToStats>();
		stats.ReportDeath();
	}

	private IEnumerator Respawn(){
		RespawnBattery();

		SetComponentsEnabled(false);
		ResetPosition();

		isRespawning = true;
		yield return new WaitForSeconds(respawnTime);
		isRespawning = false;

		SetComponentsEnabled(true);
		
		transform.forward = playerSpawn.forward;
	}

	private void RespawnBattery()
	{
		Battery bat = GetComponent<BatteryCollector> ().GetBattery ();
		if (bat == null) return;
		
		GetComponent<BatteryCollector> ().RemoveBattery ();
		bat.transform.position = GetClosestBatterySpawn(bat.transform.position);
        bat.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	private void ResetPosition()
	{
		transform.position = playerSpawn.position;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

	private void SetComponentsEnabled(bool isEnabled)
	{
		m.enabled = isEnabled;
		a.enabled = isEnabled;
		h.enabled = isEnabled;
        coll.enabled = isEnabled;

		foreach (MeshRenderer rend in GetComponentsInChildren<MeshRenderer>())
		{
			rend.enabled = isEnabled;
		}

		rb.isKinematic = !isEnabled;
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

	public bool IsRespawning()
	{
		return isRespawning;
	}
}
