using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickShake : MonoBehaviour {

	public float maxYaw = 10f;
	public float maxPitch = 10f;
	public float maxRoll = 10f;
	public float maxTranslate = 10f;

	public float decayRate = 2f;

	const float maxTrauma = 1.0f;

	[Header("Testing")]
	[Range(0, 1)]
	public float trauma = 0;

	void Update() {
		CalculateShake();
		DecayTrauma();
	}

	/// <summary>
	/// Add trauma in range [0, 1] to shake the camera.
	/// </summary>
	public void AddTrauma(float diff)
	{
		if (trauma < 0) return;

		trauma += diff;

		if (trauma > maxTrauma)
		{
			trauma = maxTrauma;
		}
	}

	void CalculateShake()
	{
		float xCoord = Mathf.PingPong(Time.time, 1) * 50;
		float yCoord = Mathf.PingPong(Time.time, 1) * 50;

		float shake = trauma * trauma;
		float yaw = maxYaw * shake * (Mathf.PerlinNoise(xCoord, 0) * 2 - 1);
		float pitch = maxPitch * shake * (Mathf.PerlinNoise(xCoord, yCoord) * 2 - 1);
		float roll = maxPitch * shake * (Mathf.PerlinNoise(0, yCoord) * 2 - 1);

		float x = maxTranslate * shake * (Mathf.PerlinNoise(0, -yCoord) * 2 - 1);
		float y = maxTranslate * shake * (Mathf.PerlinNoise(-xCoord, -yCoord) * 2 - 1);

		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
		transform.localPosition = new Vector3 (x, y, 0.0f);
	}

	void DecayTrauma()
	{
		if (trauma > 0)
		{
			trauma -= Mathf.Min(decayRate, trauma) * Time.deltaTime;
		}
	}
}

