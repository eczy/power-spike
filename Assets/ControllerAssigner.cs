﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ControllerAssigner : MonoBehaviour {

    [Tooltip("Assign players in order to become active")]
    public Player[] players;
    public int requiredPlayers = 4;
    public string nextScene;
    public float deviceTimeout = 10f;

    Dictionary<int, float> deviceActiveTime;
    Dictionary<int, bool> deviceActive;
    Animator cameraAnimation;
    DynamicCamera cameraFollow;

    int activePlayers = 0;
    bool playersActive = false;

    private void Awake()
    {
        PlayerAssignment assigns = GameObject.FindObjectOfType<PlayerAssignment>();

        if (assigns)
        {
            foreach (Player player in players)
            {
                Team team = player.team;
                int charge = player.GetComponent<Charge>().charge;

                assigns.StorePlayerAssignment(team, charge, players.Length + 1);
            }
        }
    }

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

            if (!IsDeviceActive(i) && device.Action1.WasPressed)
            {
                AssignDevicePlayer(i);
            }
        }
    }

    void ManagePlayers()
    {
        foreach (Player player in players)
        {
            if (player.player_number < InputManager.Devices.Count)
            {
                UpdatePlayer(player);
            }
        }
    }

    void UpdatePlayer(Player activePlayer)
    {
        int deviceNum = activePlayer.player_number;

        if (deviceNum > InputManager.Devices.Count) return;

        InputDevice device = InputManager.Devices[deviceNum];

        if (device.AnyButton.WasPressed || device.LeftStickX != 0.0f)
        {
            SetDeviceActive(deviceNum);
        }

        if (IsDeviceActive(deviceNum) && (device.Action4.WasPressed || deviceActiveTime[deviceNum] <= 0.0f))
        {
            UnassignDevicePlayer(deviceNum, activePlayer);
        }
        else
        {
            deviceActiveTime[deviceNum] -= Time.deltaTime;
        }
    }

    void AssignDevicePlayer(int deviceNum)
    {
        Player newPlayer = FindFirstInactivePlayer();

        SetDeviceActive(deviceNum);
        newPlayer.player_number = deviceNum;
        activePlayers++;
    }

    void UnassignDevicePlayer(int deviceNum, Player removedPlayer)
    {
        SetDeviceInactive(deviceNum);
        removedPlayer.player_number = players.Length + 1;
        activePlayers--;
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
        deviceActiveTime = new Dictionary<int, float>();
        deviceActive = new Dictionary<int, bool>();
        
        for (int i = 0; i < InputManager.Devices.Count; i++)
        {
            deviceActive[i] = false;
            deviceActiveTime[i] = 0.0f;
        }
    }

    bool IsDeviceActive(int deviceNum)
    {
        return deviceActive[deviceNum];
    }

    void SetDeviceActive(int deviceNum)
    {
        deviceActive[deviceNum] = true;
        deviceActiveTime[deviceNum] = deviceTimeout;
    }

    void SetDeviceInactive(int deviceNum)
    {
        deviceActive[deviceNum] = false;
        deviceActiveTime[deviceNum] = 0.0f;
    }

    Player FindFirstInactivePlayer()
    {
        foreach (Player player in players)
        {
            if (player.player_number > players.Length)
            {
                return player;
            }
        }

        return null;
    }
}
