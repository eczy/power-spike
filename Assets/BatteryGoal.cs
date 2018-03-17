using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryGoal : MonoBehaviour {

	public int maxBatteries = 5;

	int currentBatteries = 0;

	public GameObject batteryPrefab;

	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update() {
		anim.SetFloat ("numBatteries", currentBatteries);
	}

	void OnTriggerStay(Collider other) {
		BatteryCollector collector = other.GetComponent<BatteryCollector> ();

		if (collector && collector.CanDrop()) {
			Battery battery = collector.GetBattery ();
			collector.RemoveBattery ();
			AddBattery (battery);

		}
		else if (collector && collector.CanGrab()) {
			Battery battery = RemoveBattery ();
			collector.TakeBattery (battery);
		}
	}

	void AddBattery(Battery battery) {
		battery.SetOwner (gameObject);
		currentBatteries += 1;
		Destroy (battery.gameObject);
	}

	Battery RemoveBattery() {
		currentBatteries -= 1;
		return Instantiate (batteryPrefab).GetComponent<Battery>();
	}
}
