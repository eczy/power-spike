using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ControllerAssigner : MonoBehaviour {

    [Tooltip("Assign players in order to become active")]
    public Player[] players;
    public int requiredPlayers = 4;
    public string nextScene;

    Dictionary<int, bool> isDeviceActive;
    Animator cameraAnimation;
    DynamicCamera cameraFollow;

    int activePlayers = 0;
    bool playersActive = false;

    void Start()
    {
        InitializeDevices();

        cameraAnimation = Camera.main.transform.parent.GetComponent<Animator>();
        cameraFollow = Camera.main.transform.parent.GetComponent<DynamicCamera>();
    }

    void Update () {
        UpdateControllers();
        ManagePlayers();
        ManageCamera();

        if (activePlayers >= requiredPlayers)
        {
            SceneTransitionController.RequestSceneTransition(nextScene, 2f);
        }
	}
    
    void UpdateControllers()
    {
        for (int i = 0; i < InputManager.Devices.Count; i++)
        {
            InputDevice device = InputManager.Devices[i];

            if (!isDeviceActive[i] && device.Action1.WasPressed)
            {
                AssignDevicePlayer(i);
            }
        }
    }

    void AssignDevicePlayer(int deviceNum)
    {
        Debug.Log("Player has joined");
        isDeviceActive[deviceNum] = true;
        players[activePlayers].player_number = deviceNum;
        activePlayers++;
    }

    void UnassignDevicePlayer(int playerNum)
    {
        Debug.Log("Player has left");
        isDeviceActive[players[playerNum].player_number] = false;
        players[playerNum].player_number = players.Length + 1;
        activePlayers--;
    }

    void ManagePlayers()
    {
        foreach (Player player in players)
        {
            int playerNum = player.player_number;


            if (playerNum < InputManager.Devices.Count)
            {
                UpdatePlayer(playerNum);
            }

        }
    }

    void UpdatePlayer(int playerNum)
    {
        if (playerNum > InputManager.Devices.Count) return;

        InputDevice device = InputManager.Devices[playerNum];

        if (isDeviceActive[playerNum] && device.Action4.WasPressed)
        {
            UnassignDevicePlayer(playerNum);
        }
    }

    void ManageCamera()
    {
        if (!playersActive && activePlayers > 0)
        {
            playersActive = true;
            cameraAnimation.enabled = false;
            cameraFollow.enabled = true;
        }
        else if (playersActive && activePlayers <= 0)
        {
            playersActive = false;
            cameraAnimation.enabled = true;
            cameraFollow.enabled = false;
        }
    }

    void InitializeDevices()
    {
        isDeviceActive = new Dictionary<int, bool>();
        for (int i = 0; i < InputManager.Devices.Count; i++)
        {
            isDeviceActive[i] = false;
        }
    }
}
