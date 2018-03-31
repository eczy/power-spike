using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text countDown;
    public float timeLimit = 180;
    
    private Overtime overtime;
    
    private void Start () {
        gameObject.SetActive(true);
        overtime = FindObjectOfType<Overtime>();
    }

    private void Update()
    {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            countDown.text = SecondsToMinuteSeconds((int) timeLimit);
        }
        else if (overtime)
        {
            overtime.StartOvertime();
        }

    }

    /* https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html */
    private string SecondsToMinuteSeconds(int timerSeconds)
    {
        string minutes = Mathf.Floor(timerSeconds / 60).ToString("0");
        string seconds = Mathf.Floor(timerSeconds % 60).ToString("00");
        return minutes + ":" + seconds;
    }
}
