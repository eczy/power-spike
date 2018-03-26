using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text countDown;
    public float timeLimit = 180;
    Overtime overtime;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(true);
        overtime = GameObject.Find("OvertimeHandler").GetComponent<Overtime>();
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        float currentTime = timeLimit;
        while (currentTime != 0)
        {
            if (GameObject.Find("black").GetComponent<GameOver>().isGameOver())
            {
                gameObject.SetActive(false);
                yield break;
            }
            currentTime -= Time.deltaTime;
            countDown.text = SecondsToMinuteSeconds((int)currentTime);
            yield return null;
        }
        if (GameObject.Find("black").GetComponent<GameOver>().isGameOver())
        {
            gameObject.SetActive(false);
            yield break;
        }
        overtime.StartOvertime();
        
    }

    /* https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html */
    string SecondsToMinuteSeconds(int timerSeconds)
    {
        string minutes = Mathf.Floor(timerSeconds / 60).ToString("0");
        string seconds = Mathf.Floor(timerSeconds % 60).ToString("00");
        return minutes + ":" + seconds;
    }
}
