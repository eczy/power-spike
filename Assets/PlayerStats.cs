using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    public Text deathText;
    public Text stealText;
    public Text captureText;

    private int deaths = 0;
    private int steals = 0;
    private int captures = 0;

    private void Update()
    {
        deathText.text = "Deaths: " + deaths.ToString();
        stealText.text = "Steals: " + steals.ToString();
        captureText.text = "Captures: " + captures.ToString();
    }

    public void Death()
    {
        deaths += 1;
    }

    public void Steal()
    {
        steals += 1;
    }

    public void Capture()
    {
        captures += 1;
    }
}
