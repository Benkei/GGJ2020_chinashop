using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public GameObject Gunfan;

    public void fanrotation (int speed)
    {
        Gunfan.transform.localRotation *= Quaternion.AngleAxis(-speed * Time.deltaTime, Vector3.up);
    }
}
