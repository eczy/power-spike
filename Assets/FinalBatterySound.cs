using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBatterySound : MonoBehaviour {
	public AudioClip alarmSound;
	AudioSource audioSource;
	BatteryGoal goal;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = alarmSound;
		goal = GetComponent<BatteryGoal> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying && goal.currentBatteries == goal.maxBatteries - 1) {
			audioSource.Play ();
		}
	}
}
