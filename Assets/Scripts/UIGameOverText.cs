using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIGameOverText : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = $"Game Over!\n You were able to save {GameplayManager.plateCount} plates out of {GameplayManager.maxPlateCount}!";
    }
}
