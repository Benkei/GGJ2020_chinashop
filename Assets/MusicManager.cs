using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static bool single = false;
    void Start()
    {
        if (single)
        {
            Destroy(gameObject);
            return;
        }

        single = true;
        DontDestroyOnLoad(gameObject);
        GetComponent<AudioSource>().Play();
    }
}
