using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertimeGoal : MonoBehaviour {

	public int maxBatteries = 5;
	public int startBatteries = 0;
	public Player.Team teamGoal;
	public int currentBatteries = 0;

	Animator anim;


	void Start () {
		anim = GetComponent<Animator> ();
		AddInitialBatteries();
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
		else if (collector && collector.CanGrab() && !CollectorOnThisTeam(collector)) {
			Battery battery = RemoveBattery ();
			if (battery)
			{
				collector.TakeBattery(battery);
			}
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

	bool CollectorOnThisTeam(BatteryCollector collector)
	{
		return collector.GetComponent<Player>().team == teamGoal;
	}

	void AddInitialBatteries()
	{
		for (int i = 0; i < startBatteries; i++)
		{
			AddBattery(Instantiate(batteryPrefab).GetComponent<Battery>());
		}
		currentBatteries = startBatteries;
	}
}
