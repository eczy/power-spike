using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {
	public BatteryGoal goal;
	public Text text;

	// Update is called once per frame
	void Update () {
		text.text = goal.GetBatteries ().ToString();
	}
}
