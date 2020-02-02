using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRot : MonoBehaviour
{  
    public GameObject Fan;
    // Start is called before the first frame update
    private void Update()
    {
        
    }

    public void Fanrotate (int rotate)
    {
        Fan.transform.localRotation *= Quaternion.AngleAxis(-rotate * Time.deltaTime, Vector3.up);
    }
}
