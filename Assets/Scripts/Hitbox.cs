using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

    public float invincibility_time = 1f;

	[Range(0,1)]
	public float traumaOnHit = 0.2f;

    bool invincible = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionStay(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (invincible)
            return;
        else
            StartCoroutine(IFrames());

        Hurtbox hurt = collision.collider.GetComponent<Hurtbox>();
        BatteryCollector collect = GetComponent<BatteryCollector>();
        Knockback knock = GetComponent<Knockback>();
        if (hurt != null)
        {
			Camera.main.GetComponent<NickShake> ().AddTrauma (traumaOnHit);
            if (collect != null && collect.GetBattery() != null) {
                collect.GetBattery().transform.position = transform.position;
                collect.GetBattery().transform.parent = null;
                collect.RemoveBattery();
            }
            if (knock != null)
            {
                knock.Knock(collision.contacts[0].point);
            }
        }
    }

    IEnumerator IFrames()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibility_time);
        invincible = false;
    }
}
