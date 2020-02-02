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
    public ParticleSystem Hitpoint;
    Vector3 hitpoint;
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
        Hitpoint.Stop();

    }
    private void Update()
    {
      
        if (fire == true)
        {
            
            if (fanSpeed <= maxFanSpeed)
            {
                fanSpeed+=8;
            }            
        }
        else
        {
            
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
                {
         
                    line.SetPosition(1, hit.point);
                    hitpoint = hit.point;
                    Hitpoint.transform.position = hitpoint;
                    Hitpoint.transform.rotation = Quaternion.LookRotation(hit.normal);
                    if (!Hitpoint.isPlaying)
                    {
                       Hitpoint.Play();
                    }

                }
                else
                {
                    line.SetPosition(1, ray.GetPoint(laserRange));
                    Hitpoint.Stop();
                }

               


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
        Hitpoint.Stop();
    }
}
