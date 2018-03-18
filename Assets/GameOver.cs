using System.Collections;
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
	public bool gameover = false;

	// Update is called once per frame
	void Update () {
		if (gameover) {
			if (InputManager.ActiveDevice.Action1.WasPressed) {
				StartCoroutine (LoadAsyncScene (main_menu_scene_name));
			}
		}
		if (redTeam.GetBatteries () == redTeam.maxBatteries) {
			gameoverText.text = "Red team wins!\nPress A to restart.";
			StartCoroutine (LerpScreen ());
		} else if (blueTeam.GetBatteries () == blueTeam.maxBatteries) {
			gameoverText.text = "Blue team wins!\nPress A to restart.";
			StartCoroutine (LerpScreen ());
		}
	}

	IEnumerator LerpScreen()
	{
		for (float time = 0; time < lerpDuration; time += Time.deltaTime) {
			black.color = Color.Lerp (Color.clear, Color.black, time/lerpDuration);
			gameoverText.color = Color.Lerp (Color.clear, Color.white, time/lerpDuration);
			yield return null;
		}
		gameover = true;
	
		yield return null;
	}

	IEnumerator LoadAsyncScene(string scene_name){
		Debug.Log ("Loading " + scene_name);

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (scene_name);

		while (!asyncLoad.isDone)
			yield return null;
	}
}
