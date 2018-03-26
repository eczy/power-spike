using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAssignment : MonoBehaviour {

    int[] assignments;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DestroyOldAssignments();
        assignments = new int[4];
    }

    public void StorePlayerAssignment(Player.Team team, int charge, int deviceNumber)
    {
        int playerCode = GetPlayerCode(team, charge);

        assignments[playerCode] = deviceNumber;
    }

    public int GetPlayerAssignment(Player.Team team, int charge)
    {
        int playerCode = GetPlayerCode(team, charge);
        
        return assignments[playerCode];
    }

    //0 - Blue Positive
    //1 - Blue Negative
    //2 - Red Positive
    //3 - Red Negative
    int GetPlayerCode(Player.Team team, int charge)
    {
        int playerCode = 0;

        if (team == Player.Team.Blue && charge > 0)
        {
            playerCode = 0;

        }
        else if (team == Player.Team.Blue && charge < 0)
        {
            playerCode = 1;
        }
        else if (team == Player.Team.Red && charge > 0)
        {
            playerCode = 2;
        }
        else if (team == Player.Team.Red && charge < 0)
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
