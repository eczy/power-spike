using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttackRange : MonoBehaviour
{
	public bool linesEnabled = true;
	public float lineOffset;
	
	private PlayerAttack attack;
	private Falloff falloff;
	private float radius;
	private ChargeAttackRange[] charges;
	private List<LineRenderer> lines;
	private Team team;

	private void Start ()
	{
		Player player = GetComponentInParent<Player>();
		attack = GetComponentInParent<PlayerAttack>();
		falloff = GetComponentInParent<Falloff>();
		
		if (!player || !attack || !falloff)
		{
			enabled = false;
			return;
		}
		
		team = player.team;
		charges = FindObjectsOfType<ChargeAttackRange>();
		
		InitLineArray();
	}
	
	private void Update ()
	{
		DrawLines();
	}

	private void DrawLines()
	{
		for (int i = 0; i < charges.Length; i++)
		{
			DrawLine(charges[i], lines[i]);
		}
	}

	private void DrawLine(Component other, LineRenderer line)
	{
		Vector3 position = transform.position;
		Vector3 destPosition = other.transform.position;

		position.y += team == Team.Blue ? lineOffset : -1 * lineOffset;
		destPosition.y += team == Team.Blue ? lineOffset : -1 * lineOffset;
		
		line.SetPosition(0, position);
		line.SetPosition(1, destPosition);

		line.enabled = DestinationInRange(other) && attack.ChargeAttackReady() && linesEnabled && !falloff.IsRespawning();
	}

	private void InitLineArray()
	{
		LineRenderer line = GetComponentInChildren<LineRenderer>();

		lines = new List<LineRenderer> {line};

		for (int i = 0; i < charges.Length - 1; i++)
		{
			LineRenderer newLine = Instantiate(line.gameObject, transform).GetComponent<LineRenderer>();
			lines.Add(newLine);
		}
	}

	private bool DestinationInRange(Component other)
	{
		Falloff otherFalloff = other.GetComponentInParent<Falloff>();
		if (otherFalloff && otherFalloff.IsRespawning())
		{
			return false;
		}
		
		CapsuleCollider coll = other.GetComponentInParent<CapsuleCollider>();
		Vector3 destPosition;

		if (!coll)
		{
			Debug.LogWarning("Line destination does not have a capsule collider");
			destPosition = other.transform.position;
		}
		else
		{
			destPosition = coll.ClosestPoint(transform.position);
		}

		return (transform.position - destPosition).magnitude <= attack.charge_radius;
	}
}
