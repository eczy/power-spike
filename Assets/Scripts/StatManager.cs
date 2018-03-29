using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour {
    private static StatManager original;

    private void Start()
    {
        if (original == null)
        {
            original = this;
        }

        if (original != this)
        {
            Destroy(gameObject);
        }

        if (GetComponent<ResetStats>())
        {
            ResetAllStats();
        }

        DontDestroyOnLoad(gameObject);
    }

    private void ResetAllStats()
    {
        foreach (StatsToUI stat in GetComponentsInChildren<StatsToUI>()) {
            stat.ResetStats();
        }
    }
}
