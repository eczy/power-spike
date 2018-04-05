using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fist : MonoBehaviour {	
	public AudioClip jab_sound;
	public AudioClip miss_sound;
	public float jab_volume = 1f;
	public float miss_volume = 1f;

	Collider coll;
	Animator anim;
	Hurtbox hurt;

	bool hit = false;
	bool played_hit_sound = false;
	void Start () {
		coll = GetComponent<Collider> ();
		anim = GetComponent<Animator> ();
		hurt = GetComponent<Hurtbox> ();
		coll.enabled = false;
	}

	void OnCollisionEnter(Collision c){
		if (c.collider.GetComponent<Hitbox> () != null)
			coll.enabled = false;
			hit = true;
	}

	public IEnumerator Jab(Transform target, float speed, float recovery, float damage=1f){
		hurt.damage = damage;
		anim.enabled = false;
		coll.enabled = true;
        if (hit && jab_sound && !played_hit_sound)
        {
            AudioSource.PlayClipAtPoint(jab_sound, transform.position, jab_volume);
            played_hit_sound = true;
        }
        yield return null;

		float t = 0;
		Vector3 start_pos = transform.localPosition;
		Vector3 target_pos = target.transform.localPosition;
		while (t < speed) {
			if (hit && jab_sound && !played_hit_sound) {
				AudioSource.PlayClipAtPoint (jab_sound, transform.position, jab_volume);
				played_hit_sound = true;
			}
			t += Time.deltaTime;
			float progress = t / speed;
			transform.localPosition = Vector3.Lerp (start_pos, target_pos, progress);
			yield return null;
		}
		if (hit && jab_sound && !played_hit_sound) {
			AudioSource.PlayClipAtPoint (jab_sound, transform.position, jab_volume);
			played_hit_sound = true;
		}
		else if (!hit && miss_sound)
			AudioSource.PlayClipAtPoint (miss_sound, transform.position, miss_volume);
		transform.localPosition = target_pos;
		coll.enabled = false;
		t = 0;
		while (t < recovery) {
			t += Time.deltaTime;
			float progress = t / recovery;
			transform.localPosition = Vector3.Lerp (target_pos, start_pos, progress);
			yield return null;
		}
		transform.localPosition = start_pos;
		hit = false;
		anim.enabled = true;
		played_hit_sound = false;
		yield return null;
	}
}
