using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsToUI : MonoBehaviour {
    private int deaths = 0;
    private int steals = 0;
    private int captures = 0;

    public void AddDeath()
    {
        deaths += 1;
    }

    public void AddSteal()
    {
        steals += 1;
    }

    public void AddCapture()
    {
        captures += 1;
    }

    public int GetDeaths()
    {
        return deaths;
    }

    public int GetSteals()
    {
        return steals;
    }

    public int GetCaptures()
    {
        return captures;
    }

    public void ResetStats()
    {
        deaths = 0;
        steals = 0;
        captures = 0;
    }
}
