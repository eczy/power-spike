﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public Button[] buttons;
	public string[] scene_names;
	public float[] delays;
	public float input_delay = 1f;

	bool get_input = true;
	int index_active = 0;
	bool inLoadCoroutine = false;
	Coroutine co;
	Player virtualplayer;

	void Start(){
		virtualplayer = GetComponent<Player> ();
		buttons [index_active].Select ();
	}
	
	// Update is called once per frame
	void Update () {
		if (virtualplayer.device.Direction.Y < 0 && get_input) {
			index_active = (index_active + 1) % buttons.Length;
			co = StartCoroutine (DelayInput ());
		} else if (virtualplayer.device.Direction.Y > 0 && get_input) {
			index_active -= 1;
			if (index_active < 0)
				index_active = buttons.Length - 1;
			co = StartCoroutine (DelayInput ());
		}

		if (virtualplayer.device.Action1.WasPressed) {
			if (co != null)
				StopCoroutine (co);
			get_input = false;

            if (index_active == buttons.Length - 1)
            {
                Application.Quit();
                return;
            }
			if (!inLoadCoroutine) {
				StartCoroutine (LoadAsyncScene (scene_names [index_active]));
			}
		}
	}

	IEnumerator DelayInput(){
		get_input = false;
		yield return new WaitForSeconds (input_delay);
		get_input = true;
	}

	IEnumerator LoadAsyncScene(string scene_name){
		Debug.Log ("Loading " + scene_name);
		inLoadCoroutine = true;
		yield return new WaitForSeconds (delays [index_active]);

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (scene_name);

		while (!asyncLoad.isDone)
			yield return null;
		inLoadCoroutine = false;
	}
}
