﻿using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [Header("Time Parameters")]
    public int timeLimit = 180;

    [Header("Textboxes")]
    public Text countDown;
    public TimeWarning warnings;

    [Header("Colors")]
    public Color warningColor;
    
    private void Start () {
        InvokeRepeating("Countdown", 1.0f, 1.0f);
    }

    private void Update()
    {
        if (timeLimit <= 10)
        {
            countDown.color = warningColor;
        }
    }

    void Countdown()
    {
        timeLimit -= 1;
        countDown.text = SecondsToMinuteSeconds((int) timeLimit);

        if (warnings)
        {
            warnings.DisplayWarnings(timeLimit);
        }

        if (timeLimit <= 0)
        {
            CancelInvoke("Countdown");
        }

    }

    /* https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html */
    private string SecondsToMinuteSeconds(int timerSeconds)
    {
        string minutes = Mathf.Floor(timerSeconds / 60).ToString("0");
        string seconds = Mathf.Floor(timerSeconds % 60).ToString("00");
        return minutes + ":" + seconds;
    }

    public bool IsTimeOut()
    {
        return timeLimit <= 0;
    }
}
