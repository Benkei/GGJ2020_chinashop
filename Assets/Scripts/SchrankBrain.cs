using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchrankBrain : MonoBehaviour
{
	public List<PlatePoint> sockets;
	[SerializeField]
	int plateCount = 0;

	void Start()
	{
		foreach (var socket in sockets)
		{
			if (socket.filled) plateCount++;
			socket.onFilled.AddListener(() =>
			{
				plateCount++;
			});
			socket.onEmptied.AddListener(() =>
			{
				plateCount--;
			});
		}
	}

	// Update is called once per frame
	void Update()
	{

	}


	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.name);
	}
}
