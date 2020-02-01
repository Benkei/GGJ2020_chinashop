using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChinaCounterBrain : MonoBehaviour
{
	public Text textTarget;

	int oldCount = 0;

	public void Update()
	{
		if (GameplayManager.plateCount != oldCount)
		{
			oldCount = GameplayManager.plateCount;
			textTarget.text = $"{GameplayManager.plateCount}/{GameplayManager.maxPlateCount}";
		}
	}
}
