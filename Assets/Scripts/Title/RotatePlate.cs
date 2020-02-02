using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlate : MonoBehaviour
{
    float x, y, z;

    private void Start()
    {
        x = Random.Range(0.1f, 1.0f);
        y = Random.Range(0.1f, 1.0f);
        z = Random.Range(0.1f, 1.0f);

        StartCoroutine(Rotate());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private IEnumerator Rotate()
    {
        while (true)
        { 
            transform.Rotate(x, y, z, Space.Self);
            yield return new WaitForEndOfFrame();
        }
    }
}
