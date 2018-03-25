using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour {

	Rigidbody rb;
	Player p;

	[Header("Horizontal Properties")]
	public float maxSpeed = 5f;
	public float acceleration = 35f;
	public AnimationCurve accelerationMultiplierByDist;

	[Header("Vertical Properties")]
	public float jumpTime = .5f;
	public float jumpHeight = 2.5f;
	public float maxFallSpeed = 5f;
	public AnimationCurve gravityModifierBySpeed;
	public float variableJumpCutoffSpeed = 1f;

	[Header("Misc Properties")]
	public float groundedSlope = .75f;

	public float maxGroundedBuffer = .2f;
	float groundedBuffer = 0f;

	public float maxJumpBuffer = .2f;
	float jumpBuffer = 0;

    public bool canMove = true;

	void Awake() {
		p = GetComponent<Player> ();
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
        if (p.device == null)
            return;
		if (p.device.Action1.WasPressed) {
			jumpBuffer = maxJumpBuffer;
		}
	}

	void FixedUpdate() {
        if (!canMove)
        {
            rb.velocity = Vector3.zero;
            rb.position = rb.position;
        }
		Vector3 velocity = rb.velocity;
        if (p.device == null)
            return;
		// Horizontal
		float xInput = p.device.LeftStickX;
		float targetSpeed = xInput * maxSpeed;
		float xDiff = targetSpeed - velocity.x;
		float thisAcceleration = acceleration * accelerationMultiplierByDist.Evaluate(Mathf.Abs(xDiff / maxSpeed));
		float xStep = Mathf.Sign(xDiff) *
			Mathf.Min(Mathf.Abs(xDiff), thisAcceleration * Time.deltaTime);
		velocity.x += xStep;

		if(xInput != 0)
			transform.forward = new Vector3(xInput, 0, 0);

		// Gravity
		float jumpPower = 2 * jumpHeight / jumpTime;
		float gravity = -2 * jumpHeight / (jumpTime * jumpTime);
		gravity *= gravityModifierBySpeed.Evaluate(velocity.y / jumpPower);
		if (!p.device.Action1.IsPressed && velocity.y > variableJumpCutoffSpeed) {
			gravity *= 5;
		}
		velocity.y += gravity * Time.deltaTime;
		velocity.y = Mathf.Max(velocity.y, -maxFallSpeed);

		// Jump
		if (jumpBuffer > 0 && groundedBuffer > 0) {
			velocity.y = jumpPower;
			jumpBuffer = 0;
			groundedBuffer = 0;
		}

		rb.velocity = velocity;

		jumpBuffer -= Time.deltaTime;
		groundedBuffer -= Time.deltaTime;
	}

	void OnCollisionStay(Collision collision) {
		if (Vector3.Dot(collision.contacts[0].normal, Vector3.up) > groundedSlope && rb.velocity.y <= 0) {
			groundedBuffer = maxGroundedBuffer;
		}
	}
}