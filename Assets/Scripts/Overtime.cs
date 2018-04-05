using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overtime : MonoBehaviour {

    public string overtimeScene = "Overtime";

    public void StartOvertime()
    {
        FreezePlayerPositions();
        SceneTransitionController.RequestSceneTransition(overtimeScene, 1.0f);
    }

    void FreezePlayerPositions()
    {
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();

        foreach(PlayerMovement player in players)
        {
            player.canMove = false;
        }
    }
}
