using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRowSpawnBrain : MonoBehaviour
{
	public Vector3 SpawnOffset = Vector3.forward;
	public int SpawnCount = 2;
	public string ResourcesFolder = "Prefabs";

	// Start is called before the first frame update
	void Start()
	{
		var prefabs = Resources.LoadAll<GameObject>(ResourcesFolder);

		Vector3 begin = Vector3.zero;
		for (int i = 0; i < SpawnCount; i++)
		{
			var prefab = prefabs[Random.Range(0, prefabs.Length)];

			if (prefab.name != "empty")
			{
				var go = Object.Instantiate(prefab);

				go.transform.parent = transform;
				go.transform.localPosition = begin;
			}

			begin += SpawnOffset;
		}
	}
}
