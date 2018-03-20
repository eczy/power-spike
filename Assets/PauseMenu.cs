using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InControl;

public class PauseMenu : MonoBehaviour {
	public GameObject pause_menu;
	public Text[] texts;
	public string[] scene_names;
	public float[] delays;
	public float input_delay = 1f;

	public bool get_input = true;
	public int index_active = 0;
	bool inLoadCoroutine = false;
	public bool paused = false;
	Coroutine co;

	void Start(){
		index_active = 0;
	}

	// Update is called once per frame
	void Update () {
		InputDevice device = InputManager.ActiveDevice;
		if (device.MenuWasPressed) {
			if (paused == false) {
				pause_menu.SetActive (true);
				Time.timeScale = 0;
			} else if (paused == true) {
				pause_menu.SetActive (false);
				Time.timeScale = 1;
			}
			paused = !paused;
		}

		if (!paused)
			return;

		for (int i = 0; i < texts.Length; i++)
		{
			texts[i].color = Color.black;
		}
		texts[index_active].color = Color.red;
		if (device.Direction.Y < 0 && get_input) {
			index_active += 1;
			if (index_active >= texts.Length)
				index_active = texts.Length-1;
			co = StartCoroutine (DelayInput ());
		} else if (device.Direction.Y > 0 && get_input) {
			index_active -= 1;
			if (index_active < 0)
				index_active = 0;
			co = StartCoroutine (DelayInput ());
		}

		if (device.Action1.WasPressed) {
			Debug.Log("Active Index: " + index_active);
			if (co != null)
				StopCoroutine (co);
			get_input = false;

			if (index_active == texts.Length - 1) {
				Debug.Log ("Exiting");
				Application.Quit ();
				return;
			} else if (index_active == 0) {
				paused = !paused;
				pause_menu.SetActive (false);
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
