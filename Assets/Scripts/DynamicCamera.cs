using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class DynamicCamera : MonoBehaviour {
	public float camera_rotate_speed = 1f;
	public float camera_follow_speed = 1f;
	public float camera_distance_factor = 1f;
	public float camera_distance_offset = 1f;
	public float camera_height_factor = 1f;
	public float camera_height_offset = 1f;
	public Transform[] players;
	public Transform midpoint;

	// Use this for initialization
	void Start () {
		midpoint.position = GetMidpointPosition ();
		transform.position = midpoint.transform.position + midpoint.transform.right * camera_distance_offset + Vector3.up * camera_height_offset;
		transform.LookAt (midpoint);
	}

	// Update is called once per frame
	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		if (inputDevice == null)
		{
			Debug.LogWarning ("WARNING: MainMenuController could not find controller!");
		}
			
		midpoint.position = GetMidpointPosition ();

		float player_distance = MaxDistFromMidpoint ();
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(midpoint.position - transform.position), Time.deltaTime * camera_rotate_speed);
		transform.position = Vector3.Lerp(transform.position, midpoint.transform.position + midpoint.transform.right * camera_distance_offset * (1 + player_distance * camera_distance_factor) + Vector3.up * camera_height_offset * (1 + player_distance * camera_height_factor), Time.deltaTime * camera_follow_speed);
	}

	Vector3 GetMidpointPosition(){
		Vector3 avg = Vector3.zero;
		foreach (Transform t in players){
			avg += t.position;
		}
		return avg / players.Length;
	}

	float MaxDistFromMidpoint(){
		float max = 0f;
		foreach (Transform t in players) {
			float d = Vector3.Distance (t.position, midpoint.position);
			if (d > max)
				max = d;
		}
		return max;
	}
}
