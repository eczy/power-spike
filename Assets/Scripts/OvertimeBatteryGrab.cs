using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertimeBatteryGrab : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerStay(Collider other)
    {
     //   BatteryCollector collector = other.GetComponent<BatteryCollector>();

//        if (collector && collector.CanGrab() && !CollectorOnThisTeam(collector))
 //       {
  //          Battery battery = Instantiate(batteryPrefab).GetComponent<Battery>();
   //         if (battery)
    //        {
     //           collector.TakeBattery(battery);
      //      }
       // }
    }
}
