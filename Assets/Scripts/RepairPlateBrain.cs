using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPlateBrain : MonoBehaviour
{
	public GameObject BrokenModel;

	public float RepairTime = 1.0f;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartRepair(Vector3 newRootPoint)
	{

		StartCoroutine(Repair(newRootPoint));
	}

	private IEnumerator Repair(Vector3 newRootPoint)
	{
		var childPos = new List<Vector3>();

		foreach (Transform child in BrokenModel.transform)
		{
			child.GetComponent<Rigidbody>().isKinematic = true;
			childPos.Add(child.position);
		}

		transform.position = newRootPoint;

		for (int i = 0; i < BrokenModel.transform.childCount; i++)
		{
			var child = BrokenModel.transform.GetChild(i);
			child.position = childPos[i];
		}

		var time = 0.0f;
		while (true)
		{
			time += Time.deltaTime;

			var blend = time / RepairTime;
			blend = Mathf.Clamp01(blend);
			foreach (Transform child in BrokenModel.transform)
			{
				child.localPosition = Vector3.Lerp(child.localPosition, Vector3.zero, blend);
				child.localRotation = Quaternion.Slerp(child.localRotation, Quaternion.identity, blend);
			}

			if(blend == 1)
			{
				break;
			}

			yield return null;
		}

	}
}
