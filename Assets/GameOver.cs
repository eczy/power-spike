﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	public string main_menu_scene_name;
	public BatteryGoal redTeam;
	public BatteryGoal blueTeam;
	public Text gameoverText;
	public Image black;
	public float lerpDuration = 1f;

	private bool screenShown = false;
    private bool gameOver = false;

    private void Start()
    {
        PlayerStats[] stats = Resources.FindObjectsOfTypeAll<PlayerStats>();

        foreach (var stat in stats)
        {
            stat.Reset();
        }
    }

    private void Update () {
		if (screenShown) {
			if (InputManager.ActiveDevice.Action1.WasPressed) {
				StartCoroutine (LoadAsyncScene (main_menu_scene_name));
			}
		}

		if (!gameOver && redTeam.GetBatteries () == redTeam.maxBatteries) {
            gameOver = true;
			gameoverText.text = "Red team wins!\nPress A to restart.";
			StartCoroutine (LerpScreen ());
		} else if (!gameOver && blueTeam.GetBatteries () == blueTeam.maxBatteries) {
            gameOver = true;
			gameoverText.text = "Blue team wins!\nPress A to restart.";
			StartCoroutine (LerpScreen ());
		}
	}

	private IEnumerator LerpScreen()
	{
		for (float time = 0; time < lerpDuration; time += Time.deltaTime) {
			black.color = Color.Lerp (Color.clear, Color.black, time/lerpDuration);
			gameoverText.color = Color.Lerp (Color.clear, Color.white, time/lerpDuration);
			yield return null;
		}
	
		screenShown = true;
        ShowStats();
	}

	IEnumerator LoadAsyncScene(string scene_name){
		Debug.Log ("Loading " + scene_name);

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (scene_name);

		while (!asyncLoad.isDone)
			yield return null;
	}

    private void ShowStats()
    {
        StatPanel[] stats = Resources.FindObjectsOfTypeAll<StatPanel>();

        foreach (var stat in stats)
        {
            stat.gameObject.SetActive(true);
        }
    }
}
