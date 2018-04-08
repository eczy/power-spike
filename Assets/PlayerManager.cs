using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    Player[] players;

	void Start () {
        players = FindObjectsOfType<Player>();
		
	}

    public void DisablePlayers()
    {
        foreach (Player player in players)
        {
            player.GetComponent<PlayerMovement>().canMove = false;
            player.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void EnablePlayers()
    {
        foreach (Player player in players)
        {
            player.GetComponent<PlayerMovement>().canMove = true;
            player.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
