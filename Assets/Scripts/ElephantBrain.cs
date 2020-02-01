using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElephantBrain : MonoBehaviour
{
	public GameObject waypointsRoot;
	public float waitTimer = 10;

	Animator animator;
	NavMeshAgent agent;

	void Awake()
	{
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}

	// Start is called before the first frame update
	IEnumerator Start()
	{
		while (true)
		{
			var count = waypointsRoot.transform.childCount;
			var child = waypointsRoot.transform.GetChild(Random.Range(0, count));

			agent.destination = child.position;

			while (agent.velocity.magnitude > 0.1f)
			{
				yield return null;
			}
			yield return new WaitForSeconds(waitTimer);
		}
	}

	void Update()
	{
		var speed = agent.velocity.magnitude;
		//var blend = speed / 1.25f;
		if (speed > 0.1f)
		{
			//animator.speed = blend;
			animator.SetBool("Walk", true);
		}
		else
		{
			//animator.speed = 1;
			animator.SetBool("Walk", false);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.name);
	}
}
