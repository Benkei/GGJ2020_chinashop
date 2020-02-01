using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatePoint : MonoBehaviour
{
    [SerializeField]
    bool _filled = false;
    public bool filled => _filled;
    public UnityEvent onFilled;
    public UnityEvent onEmptied;
    Collider plate;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Teller" && !other.attachedRigidbody.isKinematic && !filled)
        {
            other.attachedRigidbody.isKinematic = true;
            _filled = true;
            other.tag = "Untagged";
            plate = other;
            onFilled?.Invoke();
            StartCoroutine(Snap(other));
        }
    }

    IEnumerator Snap(Collider other)
    {
        float t = 0;
        var start = other.transform.position;
        var startRot = other.transform.rotation;
        while (t < 1 && other.attachedRigidbody.isKinematic)
        {
            other.transform.position = Vector3.Lerp(start, transform.position, t);
            other.transform.rotation = Quaternion.Slerp(startRot, transform.rotation, t);
            t += Time.deltaTime;
            yield return null;
        }
        other.transform.position = transform.position;
        other.transform.rotation = transform.rotation;
    }
}
