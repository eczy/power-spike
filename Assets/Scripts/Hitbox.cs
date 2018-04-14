using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hitbox : MonoBehaviour
{

	public float punchInvincibilityTime;
	public float chargeInvincibilityTime;
	public float deathInvincibilityTime;

	[Range(0,1)]
	public float traumaOnHit = 0.2f;

	private Rigidbody rb;
	private bool invincible;

	private void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionStay(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
	    if (invincible)
	    {
            return;
	    }
	    if (punchInvincibilityTime > 0) {
			StartCoroutine(MakeInvincible(punchInvincibilityTime));
		}
	    
	    Health health = GetComponent<Health> ();
        Hurtbox hurt = collision.collider.GetComponent<Hurtbox>();
        BatteryCollector collect = GetComponent<BatteryCollector>();
        Knockback knock = GetComponent<Knockback>();
        if (hurt != null)
        {
            if (health != null)
			    health.Hurt (hurt.damage);
			if (Camera.main.GetComponent<NickShake> () != null)
				Camera.main.GetComponent<NickShake> ().AddTrauma (traumaOnHit);
            if (collect != null && collect.GetBattery() != null) {
				Debug.Log ("Dropping battery!");
                collect.GetBattery().transform.position = transform.position;
                collect.GetBattery().transform.parent = null;
                collect.RemoveBattery();
            }
            if (knock != null)
            {
                knock.Knock(collision.contacts[0].normal);
            }
        }
    }

    private IEnumerator MakeInvincible(float duration)
    {
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;
    }

	public void ChargeAttackHit(Vector3 origin, float force)
	{
		if (invincible) return;
		
		rb.AddExplosionForce (force, origin, 0, 0.0f, ForceMode.Impulse);
		
		if (chargeInvincibilityTime > 0)
		{
			StartCoroutine(MakeInvincible(chargeInvincibilityTime));
		}
	}

	public void PlayerRespawn()
	{
		if (deathInvincibilityTime > 0)
		{
			StartCoroutine(MakeInvincible(deathInvincibilityTime));
		}
	}
}
