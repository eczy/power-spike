using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToStats : MonoBehaviour {
    public string statName;

    private StatsToUI stats;

    private void Start()
    {
        GameObject statsObject = GameObject.Find(statName);
        if (statsObject)
        {
            stats = statsObject.GetComponent<StatsToUI>();
        }
    }

    public void ReportDeath()
    {
        if (stats)
        {
            stats.AddDeath();
        }
    }

    public void ReportSteal()
    {
        if (stats)
        {
            stats.AddSteal();
        }
    }

    public void ReportCapture()
    {
        if (stats)
        {
            stats.AddCapture();
        }
    }
}
