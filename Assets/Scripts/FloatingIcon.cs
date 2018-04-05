using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingIcon : MonoBehaviour {

    [Header("Anchor to object")]
    public GameObject anchorObject;
    public Vector3 anchorOffset;

    [Range(0,1)]
    public float easingFactor  = 0.1f;
	
	void Update () {
        CheckObjectPosition();
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

    void SetPosition(Vector3 targetPosition)
    {
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(transform.position);
        Vector3 nextPosition = Vector3.Lerp(currentPosition, targetPosition, easingFactor);
        transform.position = Camera.main.WorldToScreenPoint(nextPosition);
    }
}
