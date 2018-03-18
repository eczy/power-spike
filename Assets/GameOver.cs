using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	public BatteryGoal redTeam;
	public BatteryGoal blueTeam;
	public Text gameoverText;
	public Image black;
	public float lerpDuration = 1f;

	// Update is called once per frame
	void Update () {
		if (redTeam.GetBatteries () == redTeam.maxBatteries) {
			gameoverText.text = "Red team wins!";
			StartCoroutine (LerpScreen ());
		} else if (blueTeam.GetBatteries () == blueTeam.maxBatteries) {
			gameoverText.text = "Blue team wins!";
			StartCoroutine (LerpScreen ());
		}
	}

	IEnumerator LerpScreen()
	{
		for (float time = 0; time < lerpDuration; time += Time.deltaTime) {
			black.color = Color.Lerp (Color.clear, Color.black, Time.deltaTime/lerpDuration);
			yield return null;
		}
	
		yield return null;
	}
}
