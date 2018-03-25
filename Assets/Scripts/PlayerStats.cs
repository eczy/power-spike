using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    public string statsName;

    public Text deathText;
    public Text stealText;
    public Text captureText;

    private StatsToUI stats;

    private void Start()
    {
        stats = GameObject.Find(statsName).GetComponent<StatsToUI>();
    }

    private void Update()
    {
        deathText.text   = stats.GetDeaths().ToString();
        stealText.text   = stats.GetSteals().ToString();
        captureText.text = stats.GetCaptures().ToString();
    }
}
