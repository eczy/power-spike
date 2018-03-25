using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanel : MonoBehaviour {

	private void Start () {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
	}
}
