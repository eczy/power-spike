using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Player : MonoBehaviour {
	[HideInInspector] public InputDevice device;
	public int player_number = 0;
	public Team team;

	private void Start () {
        PlayerAssignment assignments = FindObjectOfType<PlayerAssignment>();

        if (assignments)
        {
            Charge charge = GetComponent<Charge>();
            player_number = assignments.GetPlayerAssignment(team, charge.charge);
        }

        UpdateDevice();
	}

	private void Update () {
        UpdateDevice();
	}

	private void UpdateDevice()
    {
		if (player_number >= 0 && InputManager.Devices.Count > player_number) {
			device = InputManager.Devices [player_number];
		} else {
			device = null;
		}

    }
}