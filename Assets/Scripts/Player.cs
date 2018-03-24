using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Player : MonoBehaviour {
	[HideInInspector] public InputDevice device;
	public int player_number = 0;
	public enum Team { Blue, Red };
	public Team team;

	void Start () {
        PlayerAssignment assignments = FindObjectOfType<PlayerAssignment>();

        if (assignments)
        {
            Charge charge = GetComponent<Charge>();
            player_number = assignments.GetPlayerAssignment(team, charge.charge);
        }

        UpdateDevice();
	}

	void Update () {
        UpdateDevice();
	}

    void UpdateDevice()
    {
		if (InputManager.Devices.Count > player_number) {
			device = InputManager.Devices [player_number];
		} else {
			device = null;
		}

    }
}
