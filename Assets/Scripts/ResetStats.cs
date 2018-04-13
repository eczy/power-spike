using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStats : MonoBehaviour {

	private void Start () {
		StatManager.ResetAllStats();
	}
	
}
