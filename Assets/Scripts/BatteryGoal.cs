using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryGoal : MonoBehaviour {

	public int maxBatteries = 5;
	public int startBatteries = 0;
	public Player.Team teamGoal;
	int currentBatteries = 0;

	public GameObject batteryPrefab;

	Animator anim;
	bool alarmPlaying = false;
	public AudioClip alarmSound;
	AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.clip = alarmSound;
		anim = GetComponent<Animator> ();
        for (int i = 0; i < startBatteries; i++)
        {
            AddBattery(Instantiate(batteryPrefab).GetComponent<Battery>());
        }
		currentBatteries = startBatteries;
	}

	void Update() {
		anim.SetFloat ("numBatteries", currentBatteries);
		if (!audio.isPlaying && currentBatteries == maxBatteries - 1) {
			audio.Play ();
		}
	}

	void OnTriggerStay(Collider other) {
		BatteryCollector collector = other.GetComponent<BatteryCollector> ();

		if (collector && collector.CanDrop()) {
			Battery battery = collector.GetBattery ();
			collector.RemoveBattery ();
			AddBattery (battery);

		}
		else if (collector && collector.CanGrab() && collector.gameObject.GetComponent<Player>().team != teamGoal) {
			Battery battery = RemoveBattery ();
			if (battery == null)
				return;
			collector.TakeBattery (battery);
		}
	}

	void AddBattery(Battery battery) {
		battery.SetOwner (gameObject);
		currentBatteries += 1;
		Destroy (battery.gameObject);
	}

	Battery RemoveBattery() {
		if (currentBatteries == 0)
			return null;
		currentBatteries -= 1;
		return Instantiate (batteryPrefab).GetComponent<Battery>();
	}

	public int GetBatteries(){
		return currentBatteries;
	}
}
