using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
	{

		Debug.Log("Muhahahha");

		SceneManager.LoadScene("Scene1");

	}

	void Start()
	{

	}

	void Update()
	{

	}
}
