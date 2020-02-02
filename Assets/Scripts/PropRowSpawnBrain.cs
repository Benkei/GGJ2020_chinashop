using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PropRowSpawnBrain : MonoBehaviour
{
	public Vector3 SpawnOffset = Vector3.forward;
	public int SpawnCount = 2;
	public string ResourcesFolder = "Prefabs";

	// Start is called before the first frame update
	void Awake()
	{
		var prefabs = Resources.LoadAll<GameObject>(ResourcesFolder);

		Vector3 begin = Vector3.zero;
		int i = 0;
		for (i = 0; i < SpawnCount; i++)
		{
			var prefab = prefabs[Random.Range(0, prefabs.Length)];

			var brain = prefab.GetComponentInChildren<PlateBrain>();

			if (!prefab.name.StartsWith("empty"))
			{
				Spawn(begin, prefab);
			}

			begin += SpawnOffset;

			if (brain != null)
			{
				i++;
				break;
			}
		}

		// filter
		prefabs = prefabs.Where((a) => a.GetComponentInChildren<PlateBrain>() == null).ToArray();

		// fill none interactive props
		for (; i < SpawnCount; i++)
		{
			var prefab = prefabs[Random.Range(0, prefabs.Length)];

			if (prefab.name != "empty")
			{
				Spawn(begin, prefab);
			}

			begin += SpawnOffset;
		}
	}

	private void Spawn(Vector3 begin, GameObject prefab)
	{
		var go = Object.Instantiate(prefab);

		go.transform.parent = transform;
		go.transform.localPosition = begin;
		go.transform.localRotation = Quaternion.identity;
	}
}
