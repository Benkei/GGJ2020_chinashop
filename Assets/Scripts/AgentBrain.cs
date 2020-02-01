using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentBrain : MonoBehaviour
{
	public GameObject Target;
	
	void Awake()
	{
		var agent = GetComponent<NavMeshAgent>();

		var path = new NavMeshPath();
		agent.CalculatePath(Target.transform.position, path);

		agent.SetPath(path);
	}

	void Start()
	{

	}

	void Update()
	{

	}
}
