using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlateBar : MonoBehaviour
{
    public RectTransform lowerbar;

    void Update()
    {
        float percent = 1 - Mathf.Clamp((GameplayManager.maxPlateCount - GameplayManager.plateCount) / 100.0f, 0, 1);
        Debug.Log(GameplayManager.maxPlateCount - GameplayManager.plateCount);
        var vec = lowerbar.anchorMax;
        vec.x = percent;
        lowerbar.anchorMax = vec;
    }
}
