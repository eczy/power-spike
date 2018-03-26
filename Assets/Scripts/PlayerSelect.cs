using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class PlayerSelect : MonoBehaviour {

	public float radius = 1f;
	public RectTransform[] player_buttons;
	public RectTransform[] player_select_areas;
	public bool auto_select = false;
	[Header("Order: red pos, red neg, blue pos, blue neg")]
	public Player[] players;
	public Text press_start_text;
	public float text_switch_delay = 1f;

    PlayerAssignment assignments;
	bool[] locked;
	int[] selections;
	bool finished = false;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < players.Length; i++) {
			players [i].player_number = 100;
		}

		locked = new bool[players.Length];
		for (int i = 0; i < locked.Length; i++) {
			locked [i] = false;
		}

		selections = new int[players.Length];
		for (int i = 0; i < selections.Length; i++) {
			selections [i] = 100;
		}

		if (auto_select) {
			for (int i = 0; i < selections.Length; i++) {
				selections [i] = i;
			}
			Finish ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (finished)
			return;
		
		for (int i = 0; i < InputManager.Devices.Count; i++) {
			InputDevice d = InputManager.Devices [i];
			if (locked [i]) {
				if (d.Action2.WasPressed) {
					locked [i] = false;
					selections [i] = 100;
				}
				else
					continue;
			}
			
			player_buttons [i].localPosition = new Vector3 (d.LeftStickX, d.LeftStickY, 0).normalized * radius;
			if (d.Action1.WasPressed && d.LeftStick.Vector.magnitude > 0) {
				player_buttons [i].localPosition = new Vector3 (Mathf.Sign (d.LeftStickX), Mathf.Sign (d.LeftStickY), 0).normalized * radius;

				// Top Right
				if (Mathf.Sign (d.LeftStickX) > 0 && Mathf.Sign (d.LeftStickY) > 0) {
					bool already_exists = false;
					for (int j = 0; j < selections.Length; ++j)
						if (selections [j] == 2)
							already_exists = true;
					if (already_exists)
						continue;
					selections [i] = 2;
					locked [i] = true;
				}
				// Top Left
				else if (Mathf.Sign (d.LeftStickX) < 0 && Mathf.Sign (d.LeftStickY) > 0) {
					bool already_exists = false;
					for (int j = 0; j < selections.Length; ++j)
						if (selections [j] == 0)
							already_exists = true;
					if (already_exists)
						continue;
					selections [i] = 0;
					locked [i] = true;
				}
				// Bottom Right
				else if (Mathf.Sign (d.LeftStickX) > 0 && Mathf.Sign (d.LeftStickY) < 0) {
					bool already_exists = false;
					for (int j = 0; j < selections.Length; ++j)
						if (selections [j] == 3)
							already_exists = true;
					if (already_exists)
						continue;
					selections [i] = 3;
					locked [i] = true;
				}
				// Bottom Left
				else if (Mathf.Sign (d.LeftStickX) < 0 && Mathf.Sign (d.LeftStickY) < 0) {
					bool already_exists = false;
					for (int j = 0; j < selections.Length; ++j)
						if (selections [j] == 1)
							already_exists = true;
					if (already_exists)
						continue;
					selections [i] = 1;
					locked [i] = true;
				}
			}
		}
        int selected = 0;
        for (int i = 0; i < selections.Length; i++)
        {
            if (selections[i] != 100)
                selected++;
        }
        if (selected < 1)
            return;

		if (InputManager.ActiveDevice.MenuWasPressed)
			StartCoroutine(Finish ());
	}

	IEnumerator Finish(){
		finished = true;
		foreach (RectTransform r in player_buttons)
			r.gameObject.SetActive (false);
		foreach (RectTransform r in player_select_areas)
			r.gameObject.SetActive (false);

        press_start_text.transform.position = Camera.main.WorldToScreenPoint(Vector3.zero);
		press_start_text.text = "READY";
		yield return new WaitForSeconds (text_switch_delay);
		press_start_text.color = Color.red;
		press_start_text.text = "GO";

		for (int i = 0; i < players.Length; i++) {
            if (selections[i] != 100)
			    players [selections[i]].player_number = i;
		}

        assignments = FindObjectOfType<PlayerAssignment>();
        foreach (Player player in players)
        {
            if (player.player_number != 100)
            {
                Charge charge = player.GetComponent<Charge>();
                assignments.StorePlayerAssignment(player.team, charge.charge, player.player_number);
                player.gameObject.SetActive(true);
            }
        }

		yield return new WaitForSeconds (text_switch_delay);
		press_start_text.gameObject.SetActive (false);

        FindObjectOfType<TutorialManager>().enabled = true;
	}
}
