using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
	public static int maxPlateCount = 0;
	public static int plateCount = 0;

	static float startTime;
	public float _maxTime = 300;

	public static float currentTime => Time.time - startTime;
	public static float maxTime;

	public float _elephantStamina = 100;
	public static float currentElephantStamina;
	public static float maxElephantStamina;
	public static bool elephantEnrage;

	public UnityEvent onGameOver;

    // Start is called before the first frame update
    void Awake()
    {
		startTime = Time.time;
		maxPlateCount = 0;
		plateCount = 0;
		maxTime = _maxTime;
		currentElephantStamina = maxElephantStamina = _elephantStamina;
		elephantEnrage = false;
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
		if (elephantEnrage)
		{
			currentElephantStamina = Mathf.Max(0, currentElephantStamina - Time.deltaTime);
			if (currentElephantStamina <= 0)
			{
				onGameOver?.Invoke();
				enabled = false;
			}
		}
	}
}
