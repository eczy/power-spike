using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour {
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ResetAllStats()
    {
        foreach (StatsToUI stat in GetComponentsInChildren<StatsToUI>()) {
            stat.ResetStats();
        }
    }
}
