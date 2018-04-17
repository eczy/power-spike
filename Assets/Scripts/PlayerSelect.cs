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
    public int numPlayers = 4;

    public GameObject redPos;
    public GameObject redNeg;
    public GameObject bluePos;
    public GameObject blueNeg;

    private int[] playerSelectedQuadrants;

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

        if (autoSelect)
        {
            for (int i = 0; i < selections.Length; i++)
            {
                selections[i] = i;
            }
            Finish();
        }
        playerSelectedQuadrants = new int[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            playerSelectedQuadrants[i] = -1;
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

        // Stop player selection when everyone hits start
        bool selectionMade = selections.Count(t => t != -1) == numPlayers;
        if (selectionMade)
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
            float zPos = playerButtons[deviceIndex].localPosition.z;
            Vector3 newPosition = new Vector3(device.LeftStickX, device.LeftStickY, 0) * radius;
            newPosition.z = zPos;
            playerButtons[deviceIndex].localPosition = newPosition;
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

        playerSelectedQuadrants[deviceIndex] = quadrant;
        ChangeModelToUniqueBattery(deviceIndex, quadrant);
    }

    private void ChangeModelToUniqueBattery(int deviceIndex, int quadrant)
    {
        Transform parent = playerButtons[deviceIndex].transform;
        Transform battery = playerButtons[deviceIndex].transform.Find("battery");

        Image oldColor;
        GameObject newModel;
        // Top Left
        if (quadrant == 0)
        {
            newModel = Instantiate(redPos, parent);
            oldColor = GameObject.Find("TLArea").gameObject.GetComponent<Image>();
        }
        // Bottom Left
        else if (quadrant == 1)
        {
            newModel = Instantiate(redNeg, parent);
            oldColor = GameObject.Find("BLArea").gameObject.GetComponent<Image>();
        }
        // Top Right
        else if (quadrant == 2)
        {
            newModel = Instantiate(bluePos, parent);
            oldColor = GameObject.Find("TRArea").gameObject.GetComponent<Image>();
        }
        else
        {
            newModel = Instantiate(blueNeg, parent);
            oldColor = GameObject.Find("BRArea").gameObject.GetComponent<Image>();
        }
        Debug.Log(oldColor);
        oldColor.color = new Color(oldColor.color.r, oldColor.color.g, oldColor.color.b, 0.60f);
        newModel.transform.localScale = battery.localScale;
        battery.gameObject.SetActive(false);

    }

    private void ChangeModelToUnchosenBattery(int deviceIndex, int quadrant)
    {
        Transform player = playerButtons[deviceIndex].transform;
        Image oldColor;

        // Top Left
        if (quadrant == 0)
        {
            Destroy(player.Find("RedPosModel(Clone)").gameObject);
            oldColor = GameObject.Find("TLArea").gameObject.GetComponent<Image>();
        }
        // Bottom Left
        else if (quadrant == 1)
        {
            Destroy(player.Find("RedNegModel(Clone)").gameObject);
            oldColor = GameObject.Find("BLArea").gameObject.GetComponent<Image>();
        }
        // Top Right
        else if (quadrant == 2)
        {
            Destroy(player.Find("BluePosModel(Clone)").gameObject);
            oldColor = GameObject.Find("TRArea").gameObject.GetComponent<Image>();
        }
        else
        {
            Destroy(player.Find("BlueNegModel(Clone)").gameObject);
            oldColor = GameObject.Find("BRArea").gameObject.GetComponent<Image>();
        }

        oldColor.color = new Color(oldColor.color.r, oldColor.color.g, oldColor.color.b, 0.9f);
        player.Find("battery").gameObject.SetActive(true);
    }

    private void UnlockPlayer(int deviceIndex)
    {
        locked[deviceIndex] = false;
        selections[deviceIndex] = -1;

        playerButtons[deviceIndex].GetComponent<Image>().color = originalColor;
        ChangeModelToUnchosenBattery(deviceIndex, playerSelectedQuadrants[deviceIndex]);
        playerSelectedQuadrants[deviceIndex] = -1;
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
