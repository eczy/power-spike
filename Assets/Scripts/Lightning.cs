using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lightning : MonoBehaviour {

    public int num_segments;
    public Transform start;
    public Transform end;
    public float width = 1f;

    LineRenderer lr;

    // Use this for initialization
    void Start () {
        lr = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        lr.positionCount = num_segments;
        lr.SetPosition(0, start.localPosition);
        for (int i = 1; i < num_segments-1; i++)
        {
            Vector3 pos = Vector3.Lerp(start.localPosition, end.localPosition, (float)i / (float)num_segments);
            pos.z += Random.Range(-width, width);
            pos.y += Random.Range(-width, width);
            lr.SetPosition(i, pos);
        }
        lr.SetPosition(num_segments-1, end.localPosition);
	}
}
