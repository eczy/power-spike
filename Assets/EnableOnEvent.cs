using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnEvent : MonoBehaviour {

    public string enableEvent;

    public string disableEvent;

    void Start() {
        EventManager.On(enableEvent, EnableObject);
        EventManager.On(disableEvent, DisableObject);

        if (enableEvent != "")
        {
            DisableObject();
        }
	}

    void EnableObject()
    {
        gameObject.SetActive(true);
    }

    void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
