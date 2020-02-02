using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIGameOverText : MonoBehaviour
{
    void Start()
    {
        string text;

        if (GameplayManager.currentTime >= GameplayManager.maxTime)
        {
            text = $"Game Over!\n You were able to save {GameplayManager.plateCount} plates out of {GameplayManager.maxPlateCount}!";
        }
        else
        {
            text = $"Congratulations!\n You tired out the Elephant before the shop opened!";
        }

        GetComponent<Text>().text = text;
    }
}
