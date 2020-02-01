﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
	public static int maxPlateCount = 0;
	public static int plateCount = 0;

	static float startTime;
	public float _maxTime = 10;

	public static float currentTime => Time.time - startTime;
	public static float maxTime;

	public UnityEvent onGameOver;

    // Start is called before the first frame update
    void Start()
    {
		startTime = Time.time;
		maxPlateCount = 0;
		plateCount = 0;
		maxTime = _maxTime;
		foreach (var socket in FindObjectsOfType<PlatePoint>())
		{
			maxPlateCount++;
			if (socket.filled)
			{
				plateCount++;
			}
			socket.onFilled.AddListener(() =>
			{
				plateCount++;
			});
			socket.onEmptied.AddListener(() =>
			{
				plateCount--;
			});
		}
		Debug.Log($"Plates: {plateCount}/{maxPlateCount}");
	}

	void Update()
	{
		if (maxTime - currentTime <= 0)
		{
			onGameOver?.Invoke();
			enabled = false;
		}
	}
}
