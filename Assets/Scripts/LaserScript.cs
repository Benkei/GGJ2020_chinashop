using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserScript : MonoBehaviour
{
	LineRenderer line;
	ParticleSystem Ps;
	Light light;
	public int laserRange;
	public AudioSource loading;
	public AudioSource shooting;
	public AudioSource cooldown;
	public GameObject Fan;


	bool exit;

	void Start()
	{

		Ps = GetComponent<ParticleSystem>();
		line = GetComponent<LineRenderer>();
		light = GetComponent<Light>();
		line.enabled = false;
		light.enabled = false;
	}

	void OnFire(InputValue value)
	{
		if (value.isPressed)
		{
			Ps.Play();
			loading.Play();
			exit = true;
			StartCoroutine(FireLaser());
		}
		else
		{
			exit = false;
		}
	}
	IEnumerator FireLaser()
	{
		var wait = new WaitForEndOfFrame();
		while (exit)
		{
			if (loading.isPlaying == false)
			{
				line.enabled = true;
				light.enabled = true;

				Fan.transform.localRotation *= Quaternion.AngleAxis(-1000 * Time.deltaTime, Vector3.up);


				if (shooting.isPlaying == false)
				{
					shooting.Play();
				}

				line.material.mainTextureOffset = new Vector2(0, Time.time);
				Ray ray = new Ray(transform.position, transform.forward);
				RaycastHit hit;

				line.SetPosition(0, ray.origin);

				if (Physics.Raycast(ray, out hit, laserRange))

					line.SetPosition(1, hit.point);

				else
					line.SetPosition(1, ray.GetPoint(laserRange));

				yield return wait;
			}
			yield return null;

		}
		if (shooting.isPlaying == true)
		{
			shooting.Stop();
		}
		cooldown.Play();
		line.enabled = false;
		light.enabled = false;
		Ps.Stop();
	}
}
