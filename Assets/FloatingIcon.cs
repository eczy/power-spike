using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingIcon : MonoBehaviour {

    [Header("Anchor to object")]
    public GameObject anchorObject;
    public Vector3 anchorOffset;
    [Space(10)]

    [Header("Anchor to point")]
    public Vector3 anchorPoint;

    bool isObject = false;

    void Start () {
        isObject = anchorObject != null;
	}
	
	void Update () {
        if (isObject)
        {
            CheckObjectPosition();
        }
        else
        {
            SetPosition(anchorPoint);
        }
		
	}

    void CheckObjectPosition()
    {
        if (anchorObject == null)
        {
            Destroy(gameObject);
            return;
        }
        
        Vector3 anchorPosisition = anchorObject.transform.position + anchorOffset;
        SetPosition(anchorPosisition);
    }

    void SetPosition(Vector3 position)
    {
        transform.position = Camera.main.WorldToScreenPoint(position);
    }
}
