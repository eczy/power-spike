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
<<<<<<< HEAD
			Camera.main.GetComponent<NickShake> ().AddTrauma (traumaOnHit);
=======
			Debug.Log (gameObject.name + " was hit!");
>>>>>>> 4933795a201f2523f98f383be9149187ee676935
            if (collect != null && collect.GetBattery() != null) {
				Debug.Log ("Dropping battery!");
                collect.GetBattery().transform.position = transform.position;
                collect.GetBattery().transform.parent = null;
                collect.RemoveBattery();
            }
            if (knock != null)
            {
                knock.Knock(collision.transform.position);
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
