using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public BatteryGoal red_batteries;
	public BatteryGoal blue_batteries;
	public Transform red_start;
	public Transform blue_start;
	public Transform lightning_middle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int red = red_batteries.GetBatteries ();
		int blue = blue_batteries.GetBatteries ();
		int max = (red_batteries.maxBatteries + blue_batteries.maxBatteries);
		float p;
		if (red > blue)
			p = (float)red / max;
		else
			p = 1f - (float)blue / max;
		Debug.Log (p);
		lightning_middle.position = Vector3.Lerp (red_start.position, blue_start.position, p);
	}
}
