using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBattery : MonoBehaviour {

    public Image buttonIcon;

	void Update () {
        Transform parent = transform.parent;
        if (parent && parent.GetComponent<BatteryCollector>())
        {
            buttonIcon.enabled = false;
        }
        else
        {
            buttonIcon.enabled = true;
        }
		
	}
}
