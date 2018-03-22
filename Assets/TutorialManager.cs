using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    PlayerTutorial[] tutorials;

    bool ready = false;

	void Start () {
        tutorials = FindObjectsOfType<PlayerTutorial>();
	}
    
	void Update () {
        if (!ready && CheckPlayersReady())
        {
            Debug.Log("Ready");
        }
	}

    bool CheckPlayersReady()
    {
        foreach (PlayerTutorial player in tutorials)
        {
            if (!player.IsReady())
            {
                return false;
            }
        }
        return true;
    }
}
