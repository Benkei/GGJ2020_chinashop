using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour, IPointerClickHandler
{
	public string sceneName;
	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Muhahahha");
		SceneManager.LoadScene(sceneName);
	}

	void Start()
	{

	}

	void Update()
	{

	}
}
