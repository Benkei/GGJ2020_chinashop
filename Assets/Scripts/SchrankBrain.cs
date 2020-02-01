using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SchrankBrain : MonoBehaviour
{
	List<PlatePoint> sockets;
	[SerializeField]
	int plateCount = 0;

	void Start()
	{
		sockets = GetComponentsInChildren<PlatePoint>().ToList();
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


	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Elephant")
		{
			return;
		}
		Debug.Log(other.name);
		foreach (var socket in sockets.OrderBy(x => Random.value))
		{
			if (socket.filled)
			{
				socket.PushPlate();
				break;
			}
		}
	}
}
