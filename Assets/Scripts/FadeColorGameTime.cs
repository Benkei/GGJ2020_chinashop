using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FadeColorGameTime : MonoBehaviour
{
    public Color target;
    Color start;
    Light light;

    void Awake()
    {
        light = GetComponent<Light>();
        start = light.color;
    }

    void Update()
    {
        light.color = Color.Lerp(start, target, Mathf.Clamp(GameplayManager.currentTime / GameplayManager.maxTime, 0, 1));
    }
}
