using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseText : MonoBehaviour {

    public float speed = 1f;
    public Color color1;
    public Color color2;
    public float scale1;
    public float scale2;

    Text text;

  	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        StartCoroutine(Pulse());
	}

    IEnumerator Pulse()
    {
        float p = 0;

        while (true)
        {
            text.color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time * speed, 1));
            text.rectTransform.localScale = Vector2.Lerp(new Vector2(scale1, scale1), new Vector2(scale2, scale2), Mathf.PingPong(Time.time * speed, 1));
            yield return null;
        }
    }
}
