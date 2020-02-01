using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameTimerBrain : MonoBehaviour
{
	public Text textTarget;

	float oldTime = 0;
	TimeSpan oldSpan;

	public void Update()
	{
		if (GameplayManager.currentTime != oldTime)
		{
			oldTime = GameplayManager.currentTime;
			var span = System.TimeSpan.FromSeconds(Mathf.Max(0, GameplayManager.maxTime - GameplayManager.currentTime));
			span = new System.TimeSpan(span.Days, span.Hours, span.Minutes, span.Seconds);

			if (oldSpan != span)
			{
				oldSpan = span;
				textTarget.text = $"{oldSpan.ToString(@"mm\:ss")}";
			}
		}
	}
}
