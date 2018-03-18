using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class PlayerSelect : MonoBehaviour {

	public float radius = 1f;
	public RectTransform[] player_buttons;
	public RectTransform[] player_select_areas;
	[Header("Order: red pos, red neg, blue pos, blue neg")]
	public Player[] players;

	bool[] locked;
	int[] selections;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < players.Length; i++) {
			players [i].player_number = 100;
		}

		locked = new bool[players.Length];
		for (int i = 0; i < locked.Length; i++) {
			locked [i] = false;
		}
		foreach (bool b in locked)
			Debug.Log (b);

		selections = new int[players.Length];
		for (int i = 0; i < selections.Length; i++) {
			selections [i] = 100;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < InputManager.Devices.Count; i++) {
			InputDevice d = InputManager.Devices [i];
			if (locked [i]) {
				if (d.Action2.WasPressed)
					locked [i] = false;
				else
					continue;
			}
			
			player_buttons [i].localPosition = new Vector3 (d.LeftStickX, d.LeftStickY, 0).normalized * radius;
			if (d.Action1.WasPressed && d.LeftStick.Vector.magnitude > 0) {
				player_buttons [i].localPosition = new Vector3 (Mathf.Sign (d.LeftStickX), Mathf.Sign (d.LeftStickY), 0).normalized * radius;
				locked [i] = true;

				// Top Right
				if (Mathf.Sign (d.LeftStickX) > 0 && Mathf.Sign (d.LeftStickY) > 0)
					selections [i] = 2;
				// Top Left
				else if (Mathf.Sign (d.LeftStickX) < 0 && Mathf.Sign (d.LeftStickY) > 0)
					selections [i] = 0;
				// Bottom Right
				else if (Mathf.Sign (d.LeftStickX) > 0 && Mathf.Sign (d.LeftStickY) < 0)
					selections [i] = 3;
				// Bottom Left
				else if (Mathf.Sign (d.LeftStickX) < 0 && Mathf.Sign (d.LeftStickY) < 0)
					selections [i] = 1;
			}
		}

		// If all players are locked in
		for (int i = 0; i < locked.Length; i++) {
			if (locked [i] == false)
				return;
		}
		if (InputManager.ActiveDevice.MenuWasPressed)
			Finish ();
	}

	void Finish(){
		foreach (RectTransform r in player_buttons)
			r.gameObject.SetActive (false);
		foreach (RectTransform r in player_select_areas)
			r.gameObject.SetActive (false);

		for (int i = 0; i < players.Length; i++) {
			players [selections[i]].player_number = i;
		}
	}
}
