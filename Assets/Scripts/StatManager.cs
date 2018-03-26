using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour {
    private void Awake()
    {
        StatManager[] stats = FindObjectsOfType<StatManager>();

        foreach (StatManager stat in stats)
        {
            if (gameObject != stat.gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
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
