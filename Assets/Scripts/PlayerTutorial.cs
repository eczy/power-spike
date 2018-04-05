using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTutorial : MonoBehaviour {

    public Player player;
    
    public BatteryGoal triggerGoal;

    public GameObject punchPanel;
    public GameObject pulsePanel;
    public GameObject startPanel;

    bool ready = false;

	void Start () {
        StartCoroutine(WaitForEnable());
	}

    IEnumerator WaitForEnable()
    {
        punchPanel.SetActive(false);
        pulsePanel.SetActive(false);
        startPanel.SetActive(false);

        while (true)
        {
            if (triggerGoal.GetBatteries() > triggerGoal.startBatteries)
            {
                StartCoroutine(PunchTutorialState());
                yield break;
            }
            
            yield return null;
        }
    }

    IEnumerator PunchTutorialState()
    {
        punchPanel.SetActive(true);
        yield return null;

        Image buttonIcon = punchPanel.GetComponentInChildren<Image>();
        Coroutine flashRoutine = StartCoroutine(FlashYellow(buttonIcon));

        while (true)
        {
            if (player.device.RightTrigger.WasPressed)
            {
                StopCoroutine(flashRoutine);
                buttonIcon.color = Color.green;
                StartCoroutine(PulseTutorialState());
                yield break;
            }
            yield return null;
        }

    }

    IEnumerator PulseTutorialState()
    {
        pulsePanel.SetActive(true);
        yield return null;
        
        Image buttonIcon = pulsePanel.GetComponentInChildren<Image>();
        Coroutine flashRoutine = StartCoroutine(FlashYellow(buttonIcon));

        while (true)
        {
            if (player.device.LeftTrigger.WasPressed)
            {
                StopCoroutine(flashRoutine);
                buttonIcon.color = Color.green;
                StartCoroutine(WaitForReady());
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator WaitForReady()
    {
        startPanel.SetActive(true);
        yield return null;

        Image buttonIcon = startPanel.GetComponentInChildren<Image>();
        Coroutine flashRoutine = StartCoroutine(FlashYellow(buttonIcon));

        while (true)
        {
            if (player.device.MenuWasPressed)
            {
                StopCoroutine(flashRoutine);
                buttonIcon.color = Color.green;
                ready = true;
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator FlashYellow(Image image)
    {
        while (true)
        {
            float flashRate = 5;
            image.color = Color.yellow;
            yield return new WaitForSeconds(1 / flashRate);
            image.color = Color.white;
            yield return new WaitForSeconds(1 / flashRate);
        }
    }

    public bool IsReady()
    {
        return ready;
    }
}
