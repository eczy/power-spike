using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OneWayColliderDown : MonoBehaviour {
    public Collider platform;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player>().device.LeftStickY < -0.5)
        {
            Physics.IgnoreCollision(platform, other, true);
        }
    }

}
