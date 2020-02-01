using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElephantBrain : MonoBehaviour
{
	public GameObject waypointsRoot;
	public float waitTimer = 10;

	// Start is called before the first frame update
	IEnumerator Start()
	{
		var agent = GetComponent<NavMeshAgent>();

		while (true)
		{
			var count = waypointsRoot.transform.childCount;
			var child = waypointsRoot.transform.GetChild(Random.Range(0, count));

			agent.destination = child.position;

			yield return new WaitForSeconds(waitTimer);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.name);
	}
}
