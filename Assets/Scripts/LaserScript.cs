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
    public int minFanSpeed;
    public int maxFanSpeed;
    int fanSpeed;
    Fan fan;
	bool fire;

	void Start()
	{
        fan = GetComponent<Fan>();
		Ps = GetComponent<ParticleSystem>();
		line = GetComponent<LineRenderer>();
		light = GetComponent<Light>();
		line.enabled = false;
		light.enabled = false;
        fanSpeed = minFanSpeed;
        
    }
    private void Update()
    {
        Debug.Log(fanSpeed);
        Debug.Log(fire);
        if (fire == true)
        {
            Debug.Log("Schuss");
            if (fanSpeed <= maxFanSpeed)
            {
                fanSpeed+=8;
            }            
        }
        else
        {
            Debug.Log("kein Schuss");
            if (fanSpeed > minFanSpeed)
            {
                fanSpeed-=8;
            }
        }   

        if (minFanSpeed >= fanSpeed)
        {
            fan.fanrotation(minFanSpeed);
        }
        else
        {
            fan.fanrotation(fanSpeed);
        }
    }

    void OnFire(InputValue value)
	{
		if (value.isPressed)
		{
			Ps.Play();
			loading.Play();
			fire = true;
			StartCoroutine(FireLaser());
		}
		else
		{
			fire = false;
		}
	}
	IEnumerator FireLaser()
	{
		var wait = new WaitForEndOfFrame();
		while (fire)
		{
			if (loading.isPlaying == false)
			{
				line.enabled = true;
				light.enabled = true;

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
