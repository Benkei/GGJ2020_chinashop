﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlateBrain : MonoBehaviour
{
	public string type;
	public GameObject BaseModel;
	public GameObject BrokenModel;
	public UnityEvent onBroke;
	public AudioSource breakSound;

	void Awake()
	{
		BaseModel.SetActive(true);
		BrokenModel.SetActive(false);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (!enabled)
		{
			return;
		}

		ExplodeModel(collision.GetContact(0).point);
	}

	public void ExplodeModel(Vector3 contact)
	{
		BaseModel.SetActive(false);

		gameObject.tag = "Teller";

		if (breakSound != null)
		{
		breakSound.Play();
		}

		if (!BrokenModel.activeSelf)
		{
			BrokenModel.SetActive(true);

			//var rigied = BrokenModel.GetComponent<Rigidbody>();
			foreach (var item in BrokenModel.GetComponentsInChildren<Rigidbody>())
			{
				//item.velocity = rigied.velocity;
				//item.angularVelocity = rigied.angularVelocity;
				item.AddExplosionForce(100, contact, 100f);
			}
			onBroke?.Invoke();
		}
	}

	public void ResetModel()
	{
		BaseModel.SetActive(true);
		BrokenModel.SetActive(false);

		foreach (Transform child in BrokenModel.transform)
		{
			child.GetComponent<Rigidbody>().isKinematic = false;
			child.localPosition = Vector3.zero;
			child.localRotation = Quaternion.identity;
		}
	}
}
