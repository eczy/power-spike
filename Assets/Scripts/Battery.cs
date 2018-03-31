using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

	public float cooldown = 2;

	bool onCooldown = false;

    void StartCooldown() {
		StartCoroutine (CooldownRoutine());
	}

	IEnumerator CooldownRoutine() {
		onCooldown = true;
		yield return new WaitForSeconds (cooldown);
		onCooldown = false;
	}

	public bool SetOwner(GameObject owner) {
		if (onCooldown)
			return false;

        ResetOwner();
		transform.parent = owner.transform;
		transform.localPosition = Vector3.up * 2;
		StartCooldown ();
        return true;
	}

    public void ResetOwner()
    {
        BatteryCollector parent = GetComponentInParent<BatteryCollector>();
        if (parent)
        {
            parent.RemoveBattery();
        } 
    }
}
