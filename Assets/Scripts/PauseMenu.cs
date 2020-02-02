using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    void OnEnable()
    {
        Time.timeScale = Mathf.Epsilon;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
