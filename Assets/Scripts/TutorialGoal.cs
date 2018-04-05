using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGoal : MonoBehaviour {

    public Image buttonImage;
    public BatteryCollector guidedPlayer;

	void Start () {
        buttonImage.enabled = false;
	}
	
	void Update () {
        if (guidedPlayer.GetBattery())
        {
            buttonImage.enabled = true;
        }
        else
        {
            buttonImage.enabled = false;
        }
		
	}
}
