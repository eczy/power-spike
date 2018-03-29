using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OneWayColliderDown : MonoBehaviour {
    public Collider platform;

    private void OnTriggerStay(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && player.device != null && player.device.LeftStickY < -0.75f)
        {
            Physics.IgnoreCollision(platform, other, true);
        }
    }

}
