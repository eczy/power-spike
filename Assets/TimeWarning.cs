﻿using System.Collections;
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

        currentText = "3";
        currentFont = numberFontSize;
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
            ShowWarning(currentText.ToString(), wordFontSize, warningSound);
        }
        else if (currentTime == secondWarning)
        {
            currentText = secondWarning.ToString() + " seconds remaining";
            ShowWarning(currentText.ToString(), wordFontSize, warningSound);
        }
        else if (currentTime <= countdownTime)
        {
            ShowWarning(currentTime.ToString(), numberFontSize, countdownSound);
        }
    }

    public void DisplayPrecount(int precount)
    {
        string text = precount.ToString();
        AudioClip clip = countdownSound;

        if (precount == 0)
        {
            text = "GO!";
            clip = warningSound;
        }
        
        ShowWarning(text, numberFontSize, clip);
    }

    void ShowWarning(string text, int fontSize, AudioClip clip)
    {
        currentText = text;
        currentFont = fontSize;
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position + 5*Vector3.forward);
        cameraShake.AddTrauma(trauma);
        anim.SetTrigger("TriggerWarning");
    }
}
