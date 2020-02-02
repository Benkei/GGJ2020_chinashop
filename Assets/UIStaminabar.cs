using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStaminabar : MonoBehaviour
{
    public RectTransform lowerbar;

    // Update is called once per frame
    void Update()
    {
        float percent = GameplayManager.currentElephantStamina / GameplayManager.maxElephantStamina;
        var vec = lowerbar.anchorMax;
        vec.x = percent;
        lowerbar.anchorMax = vec;
    }
}
