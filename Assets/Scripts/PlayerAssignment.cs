using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAssignment : MonoBehaviour {

    static PlayerAssignment singleton;

    int[] assignments;

    void Awake()
    {
        if (singleton && singleton != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        assignments = new int[4] { -1, -1, -1, -1 };
    }

    public void StorePlayerAssignment(Team team, int charge, int deviceNumber)
    {
        int playerCode = GetPlayerCode(team, charge);

        assignments[playerCode] = deviceNumber;
    }

    public int GetPlayerAssignment(Team team, int charge)
    {
        int playerCode = GetPlayerCode(team, charge);
        
        return assignments[playerCode];
    }

    //0 - Blue Positive
    //1 - Blue Negative
    //2 - Red Positive
    //3 - Red Negative
    int GetPlayerCode(Team team, int charge)
    {
        int playerCode = 0;

        if (team == Team.Blue && charge > 0)
        {
            playerCode = 0;

        }
        else if (team == Team.Blue && charge < 0)
        {
            playerCode = 1;
        }
        else if (team == Team.Red && charge > 0)
        {
            playerCode = 2;
        }
        else if (team == Team.Red && charge < 0)
        {
            playerCode = 3;
        }

        return playerCode;
    }

    void DestroyOldAssignments()
    {
        PlayerAssignment[] assignments = FindObjectsOfType<PlayerAssignment>();

        foreach(PlayerAssignment assign in assignments)
        {
            if (assign.gameObject != gameObject)
            {
                Destroy(assign.gameObject);
            }
        }
    }

}
