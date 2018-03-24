using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToStats : MonoBehaviour {
    public PlayerStats stats;

    public void ReportDeath()
    {
        if (stats)
        {
            stats.Death();
        }
    }

    public void ReportSteal()
    {
        if (stats)
        {
            stats.Steal();
        }
    }

    public void ReportCapture()
    {
        if (stats)
        {
            stats.Capture();
        }
    }
}
