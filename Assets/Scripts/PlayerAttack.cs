using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	[Header("Charge Attack Parameters")]
	public ParticleSystem charge_system;
	public float charge_duration = 1f;
	public float charge_force = 1f;
	public float cooldown = 1f;
	public AudioClip electric_sound;
	public float sound_volume = 1f;
    public float charge_radius = 1f;

	[Header("Punch Parameters")]
	public Fist left;
	public Fist right;
	public Transform punch_target;
	public float punch_speed = 1f;
	public float punch_recovery_time = 1f;
	public float punch_damage = 1f;
    public float punch_distance = 1f;

	Player p;
	Charge charge;
	ParticleSystem.EmissionModule em;

	bool charge_attacking = false;
	bool punching = false;
	bool fist_toggle = false;

	// Use this for initialization
	void Start () {
		p = GetComponent<Player> ();
		em = charge_system.emission;
		charge = GetComponent<Charge> ();

        ParticleSystem.ShapeModule shape = charge_system.shape;
        shape.radius = charge_radius;

		em.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (p.device == null)
            return;
        if (p.device.LeftTrigger.WasPressed && !charge_attacking) {
            StartCoroutine (ChargeAttack ());
		}
		if (p.device.RightTrigger.WasPressed && !punching) {
			StartCoroutine (Punch ());
		}
        if (p.device.LeftStick.X * p.device.LeftStick.Y != 0.0f)
            punch_target.transform.position = transform.position + new Vector3(p.device.LeftStickX, p.device.LeftStickY, 0).normalized * punch_distance;
	}

	public bool ChargeAttackReady()
	{
		return !charge_attacking;
	}

	IEnumerator ChargeAttack(){
		charge_attacking = true;
		em.enabled = true;

		AudioSource.PlayClipAtPoint (electric_sound, Camera.main.transform.position, sound_volume);

		Collider[] colliders = Physics.OverlapSphere(transform.position, charge_radius);
		foreach (Collider hit in colliders)
		{
			if (hit.GetComponent<Charge>() == null || hit.gameObject == this.gameObject ||
			    hit.transform.IsChildOf(this.transform))
			{
				continue;
			}
			
            Team hitTeam = hit.GetComponent<Player>().team;
            Team team = p.team;

			int multiplier = hitTeam == team ? -1 : 1;

			Hitbox hitbox = hit.GetComponent<Hitbox>();
			if (hitbox)
			{
				hitbox.ChargeAttackHit(transform.position, multiplier * charge_force);
			}
		}

		yield return new WaitForSeconds (charge_duration);

		em.enabled = false;

		yield return new WaitForSeconds (cooldown);
		charge_attacking = false;
	}

	IEnumerator Punch() {
		punching = true;
        if (fist_toggle)
            StartCoroutine(left.Jab(punch_target, punch_speed, punch_recovery_time, punch_damage));
        else
            StartCoroutine(right.Jab(punch_target, punch_speed, punch_recovery_time, punch_damage));
		fist_toggle = !fist_toggle;
        yield return new WaitForSeconds (punch_recovery_time);
		punching = false;
	}
}
