using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    public string nextScene;

    PlayerTutorial[] tutorials;

    bool ready = false;

	void Start () {
        tutorials = FindObjectsOfType<PlayerTutorial>();
	}
    
	void Update () {
        if (!ready && CheckPlayersReady())
        {
            ready = true;
            SceneTransitionController.RequestSceneTransition(nextScene, 2f);
        }
	}

    bool CheckPlayersReady()
    {
        foreach (PlayerTutorial player in tutorials)
        {
            if (!player.IsReady() && player.player.gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
}
