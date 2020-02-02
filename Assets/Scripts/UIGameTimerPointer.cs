using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameTimerPointer : MonoBehaviour
{
    public RectTransform pointer;

    void Update()
    {
        float rot = GameplayManager.timerPercentage * 180 - 90;
        pointer.transform.localEulerAngles = new Vector3(0, 0, rot);
    }
}
