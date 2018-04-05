using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeWarning : MonoBehaviour {

    [Header("Warning Times")]
    public int firstWarning = 60;
    public int secondWarning = 30;
    public int countdownTime = 10;

    [Header("Text Parameters")]
    public int wordFontSize = 80;
    public int numberFontSize = 120;

    [Header("Sound Clips")]
    public AudioClip warningSound;
    public AudioClip countdownSound;

    [Header("Juice")]
    [Range(0,1)]
    public float trauma = 1f;

    Text warningText;
    Animator anim;
    NickShake cameraShake;
    float activeTime = 0;

    string currentText;
    int currentFont;

	void Start () {
        warningText = GetComponent<Text>();
        anim = GetComponent<Animator>();
        cameraShake = Camera.main.GetComponent<NickShake>();

        currentText = "Collect!";
        currentFont = wordFontSize;
	}

    private void LateUpdate()
    {
        warningText.text = currentText;
        warningText.fontSize = currentFont;
    }

    public void DisplayWarnings(int currentTime)
    {
        if (currentTime == firstWarning)
        {
            currentText = firstWarning.ToString() + " seconds remaining";
            currentFont = wordFontSize;
            AudioSource.PlayClipAtPoint(warningSound, Camera.main.transform.position);
            cameraShake.AddTrauma(trauma);
            anim.SetTrigger("TriggerWarning");
        }
        else if (currentTime == secondWarning)
        {
            currentText = secondWarning.ToString() + " seconds remaining";
            currentFont = wordFontSize;
            AudioSource.PlayClipAtPoint(warningSound, Camera.main.transform.position);
            cameraShake.AddTrauma(trauma);
            anim.SetTrigger("TriggerWarning");
        }
        else if (currentTime <= countdownTime)
        {
            currentText = currentTime.ToString();
            currentFont = numberFontSize;
            AudioSource.PlayClipAtPoint(countdownSound, Camera.main.transform.position + 5*Vector3.forward);
            cameraShake.AddTrauma(trauma);
            anim.SetTrigger("TriggerWarning");
        }
    }
}
