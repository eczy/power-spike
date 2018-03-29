using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overtime : MonoBehaviour {
    GameObject redPos;
    GameObject redNeg;
    GameObject bluePos;
    GameObject blueNeg;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartOvertime()
    {
        InitOvertime();
        StartCoroutine(PrepareForOvertime());
    }

    IEnumerator PrepareForOvertime()
    {        // Show OVERTIME screen
             // Set team's batteries to 0
             // Add glow where batteries go
             // Spawn battery in center
            
        FreezePlayerPositions();
        StartCoroutine(FadeInOvertimeScreen());
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadSceneAsync("Overtime");
        
    }

    IEnumerator FadeInOvertimeScreen()
    {
        yield return null;
    }

    void InitOvertime()
    {
        redPos = GameObject.Find("Red Pos");
        redNeg = GameObject.Find("Red Neg");
        bluePos = GameObject.Find("Blue Pos");
        blueNeg = GameObject.Find("Blue Neg");
    }

    void FreezePlayerPositions()
    {
        redPos.GetComponent<PlayerMovement>().canMove = false;
        redNeg.GetComponent<PlayerMovement>().canMove = false;
        bluePos.GetComponent<PlayerMovement>().canMove = false;
        blueNeg.GetComponent<PlayerMovement>().canMove = false;
        
    }
}
