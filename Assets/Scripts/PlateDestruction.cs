using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateDestruction : MonoBehaviour
{
    public Collider Col;
    public Rigidbody R;
    public bool IsDestroyed = false;
    

    void OnCollisionEnter(Collision col)
    {
        IsDestroyed = true;
        R.isKinematic = false;
        Col.enabled = false;
        foreach (var rigidbody in transform.GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.isKinematic = true;
        }
    }

    //void O
    //foreach(var rigidbody in transform.GetComponentsInChildren<Rigidbody>())
    //    {
    //        rigidbody.transform.localPosition = Vector3.zero;
    //        rigidbody.transform.localRotation = Quaternion.identity;
    //    }
}
