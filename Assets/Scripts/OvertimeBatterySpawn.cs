using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertimeBatterySpawn : MonoBehaviour {
    public AnimationCurve verticalPosition;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.y = verticalPosition.Evaluate(Time.timeSinceLevelLoad);
        transform.position = pos;
	}
    private void OnTriggerEnter(Collider other)
    {
        enabled = false;
    }
}
