using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElephantBrain : MonoBehaviour
{
	public GameObject waypointsRoot;
	public float waitTimer = 10;

	public Animator animator;
	NavMeshAgent agent;

	float highPlateTime = 0;
	public float enrageStartPercentage = 0.95f;
	public float enrageEndPercentage = 0.93f;
	public float enrageSpeedIncrease = 1.5f;

	void Awake()
	{
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
			yield return new WaitForSeconds(GameplayManager.elephantEnrage ? waitTimer / 4 : waitTimer);
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

		//if (!enrage)
		if (GameplayManager.plateCount * 1.0f / GameplayManager.maxPlateCount >= enrageStartPercentage && !GameplayManager.elephantEnrage)
		{
			highPlateTime += Time.deltaTime;
			if (highPlateTime > 10 && GameplayManager.currentElephantStamina > 0)
			{
				StartCoroutine(Rage());
			}
		}
		else
		{
			highPlateTime = 0;
		}

		
	}

	IEnumerator Rage()
	{
		if (GameplayManager.elephantEnrage)
		{
			yield break;
		}
		Debug.Log("Enrage!");
		highPlateTime = 0;
		GameplayManager.elephantEnrage = true;
		float normalSpeed = agent.speed;
		agent.speed *= enrageSpeedIncrease;
		yield return new WaitWhile(() => GameplayManager.plateCount * 1.0f / GameplayManager.maxPlateCount > enrageEndPercentage && GameplayManager.currentElephantStamina > 0);
		Debug.Log("End of Enrage");
		agent.speed = normalSpeed;
		GameplayManager.elephantEnrage = false;
	}

	//void OnTriggerEnter(Collider other)
	//{
	//	//Debug.Log(other.name);
	//}

	private void OnGUI()
	{
		GUILayout.Label(GameplayManager.elephantEnrage ? "Enrage" : "Normal");
		GUILayout.Label($"Stamina: {GameplayManager.currentElephantStamina}/100");
	}
}
