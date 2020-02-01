using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateBrain : MonoBehaviour
{
	public GameObject BaseModel;
	public GameObject BrokenModel;

	void Awake()
	{
		BaseModel.SetActive(true);
		BrokenModel.SetActive(false);
	}

	void OnCollisionEnter(Collision collision)
	{
		BaseModel.SetActive(false);

		if (!BrokenModel.activeSelf)
		{
			BrokenModel.SetActive(true);
			var posi = collision.GetContact(0).point;

			//Debug.Log("explode " + posi);

			foreach (var item in BrokenModel.GetComponentsInChildren<Rigidbody>())
			{
				item.AddExplosionForce(100, posi, 100f);
			}
		}
	}


}
