using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class PlayerSelect : MonoBehaviour {

	public float radius = 1f;
	public RectTransform[] playerButtons;
	public RectTransform[] playerSelectAreas;
	public bool autoSelect;

	[Header("Order: red pos, red neg, blue pos, blue neg")]
	public Player[] players;
	public GameObject pressStartText;
    public Color selectedColor;

    private PlayerAssignment assignments;
    private bool[] locked;
    private int[] selections;
    private bool selectionDone;
    private Color originalColor;

    private void Start()
    {
		foreach (Player player in players)
		{
		    player.player_number = -1;
		}

		locked = new bool[players.Length];

		selections = new int[players.Length];
		for (int i = 0; i < selections.Length; i++) {
			selections[i] = -1;
		}

		if (autoSelect) {
			for (int i = 0; i < selections.Length; i++) {
				selections [i] = i;
			}
			Finish();
		}
	}

    private void Update()
    {
        if (!selectionDone)
        {
            UpdateSelections();
        }
	}

    private void UpdateSelections()
    {
		for (int i = 0; i < InputManager.Devices.Count; i++) {
            UpdateDevice(i);
        }

        // Stop player selection when a device presses start with at least one player selected
        bool selectionMade = selections.Count(t => t != -1) > 0;
        if (selectionMade && InputManager.ActiveDevice.MenuWasPressed)
        {
			Finish();
        }
    }

    private void UpdateDevice(int deviceIndex)
    {
        InputDevice device = InputManager.Devices[deviceIndex];

        bool selectionCancelled = locked[deviceIndex] && device.Action2.WasPressed;

        if (selectionCancelled) {
            UnlockPlayer(deviceIndex);
        }

        if (locked[deviceIndex]) return;

        bool deviceMadeSelection = device.Action1.WasPressed && device.LeftStick.Vector.magnitude > 0;

        if (deviceMadeSelection)
        {
            LockPlayer(deviceIndex);
        }
        else
        {
            playerButtons[deviceIndex].localPosition = new Vector3(device.LeftStickX, device.LeftStickY, 0) * radius;
        }

    }

    private void Finish(){
		selectionDone = true;

	    HidePlayerSelect();

		for (int i = 0; i < players.Length; i++) {
		    if (selections[i] != -1)
		    {
			    players[selections[i]].player_number = i;
		    }
		}

        assignments = FindObjectOfType<PlayerAssignment>();
        foreach (Player player in players)
        {
            if (player.player_number == -1) continue;

            Charge charge = player.GetComponent<Charge>();
            assignments.StorePlayerAssignment(player.team, charge.charge, player.player_number);
            player.gameObject.SetActive(true);
        }

        FindObjectOfType<TutorialManager>().enabled = true;
	}

    private void LockPlayer(int deviceIndex)
    {
        Vector3 direction = playerButtons[deviceIndex].localPosition.normalized;

        int quadrant = GetQuadrant(direction);

        if (PlayerSelected(quadrant)) return;

		playerButtons[deviceIndex].localPosition = new Vector3(Mathf.Sign(direction.x), Mathf.Sign(direction.y), 0).normalized * radius;
        selections[deviceIndex] = quadrant;
        locked[deviceIndex] = true;

        Image buttonImage = playerButtons[deviceIndex].GetComponent<Image>();
        originalColor = buttonImage.color;
        buttonImage.color = selectedColor;
    }

    private void UnlockPlayer(int deviceIndex)
    {
        locked[deviceIndex] = false;
        selections[deviceIndex] = -1;

        playerButtons[deviceIndex].GetComponent<Image>().color = originalColor;
    }

    private bool PlayerSelected(int player)
    {
        bool selected = false;
        foreach (int selection in selections)
        {
            if (selection == player)
            {
                selected = true;
            }
        }

        return selected;
    }

    private int GetQuadrant(Vector3 direction)
    {

        int quadrant = -1;

        // Top Right
        if (direction.x > 0 && direction.y > 0)
        {
            quadrant = 2;
        }
        // Top Left
        else if (direction.x < 0 && direction.y > 0)
        {
            quadrant =  0;
        }
        // Bottom Right
        else if (direction.x > 0 && direction.y < 0)
        {
            quadrant = 3;
        }
        // Bottom Left
        else if (direction.x < 0 && direction.y < 0)
        {
            quadrant = 1;
        }

        return quadrant;
    }

	private void HidePlayerSelect()
	{
        foreach (RectTransform r in playerButtons)
        {
			r.gameObject.SetActive (false);
        }

        foreach (RectTransform r in playerSelectAreas)
        {
			r.gameObject.SetActive (false);
        }
		
		pressStartText.SetActive(false);
	}
}
