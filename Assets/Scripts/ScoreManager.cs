using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public BatteryGoal red_batteries;
	public BatteryGoal blue_batteries;
	public Transform red_start;
	public Transform blue_start;
	public Transform lightning_middle;
	public AudioClip score_change_sound;
	public float score_change_volume = 1f;
    public bool frantic = false;
    public float frantic_speed = 1f;

	int prev_blue = 0;
	int prev_red = 0;

    private void Start()
    {
        if (frantic)
            StartCoroutine(FranticMode());
    }

    // Update is called once per frame
    void Update () {
        if (frantic)
            return;

		int red = red_batteries.GetBatteries ();
		int blue = blue_batteries.GetBatteries ();

		if (red > prev_red || blue > prev_blue) {
			Camera.main.GetComponent<NickShake> ().AddTrauma (0.7f);
			AudioSource.PlayClipAtPoint (score_change_sound, Camera.main.transform.position, score_change_volume);
		}
		float p;
		if (red > blue)
			p = (float)red / (red+blue);
		else
			p = 1f - (float)blue / (red+blue);
		lightning_middle.position = Vector3.Lerp (red_start.position, blue_start.position, p);
		prev_red = red;
		prev_blue = blue;
	}

    IEnumerator FranticMode()
    {
        while (true)
        {
            float r = Random.Range(0f, 1f);
            lightning_middle.position = Vector3.Lerp(lightning_middle.position, Vector3.Lerp(red_start.position, blue_start.position, r), Time.deltaTime * frantic_speed);
            yield return null;
        }
    }
}
