using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(LineRenderer)]
public class Lightning : MonoBehaviour {

    public int num_segments;

    LineRenderer lr;

    // Use this for initialization
    void Start () {
        lr = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
