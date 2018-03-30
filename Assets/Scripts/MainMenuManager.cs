using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public Text[] texts;
	public string[] scene_names;
	public float[] delays;
	public float input_delay = 1f;

	bool get_input = true;
	public int index_active = 0;
	bool inLoadCoroutine = false;
	Coroutine co;

	void Start(){
        index_active = 0;
	}
	
	// Update is called once per frame
	void Update () {
        InputDevice device = InputManager.ActiveDevice;
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

			if (!inLoadCoroutine) {
                SceneTransitionController.RequestSceneTransition(scene_names[index_active], 2f);
			}
		}
	}

	IEnumerator DelayInput(){
		get_input = false;
		yield return new WaitForSeconds (input_delay);
		get_input = true;
	}
}
