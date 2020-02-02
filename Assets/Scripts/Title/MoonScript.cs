using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moveMoon());
    }


    private IEnumerator moveMoon()
    { 
        while(true)
        {
            transform.position += new Vector3(0.005f, 0.005f,0.00f);
            yield return new WaitForEndOfFrame();

        }
    }
}
