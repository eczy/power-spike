using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCollector : MonoBehaviour {

	public float grabBuffer = 0.2f;

	float currentBuffer = 0.0f;
	Battery thisBattery = null;
	Player player;

	void Start() {
		player = GetComponent<Player> ();
	}

	void Update() {
		if (player.device != null && player.device.Action2.WasPressed) {
			currentBuffer = grabBuffer;
		} else {
			currentBuffer -= Time.deltaTime;
		}
	}

	void OnTriggerStay(Collider other)
	{
		Battery battery = other.gameObject.GetComponent<Battery> ();
		if (battery && !HasBattery()) {
			if (currentBuffer > 0.0f) {
				TakeBattery (battery);
				currentBuffer = 0;
			}
		}
	}

	public bool HasBattery() {
		return thisBattery != null;
	}

	public Battery GetBattery() {
		return thisBattery;
	}

	public void RemoveBattery() {
		thisBattery.transform.parent = null;
		thisBattery = null;
		currentBuffer = 0;
	}

	public void TakeBattery(Battery battery) {
		if (battery.SetOwner (gameObject))
        {
		    thisBattery = battery;
		    currentBuffer = 0;
            EventManager.Trigger("BatteryPickupEvent");
        }
	}

	public bool CanGrab() {
		return !HasBattery() && currentBuffer > 0.0f;
	}

	public bool CanDrop() {
		return HasBattery () && currentBuffer > 0.0f;
	}
}
