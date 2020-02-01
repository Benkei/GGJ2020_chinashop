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
    PlateBrain plate;
    Collider triggerCol;

    private void Start()
    {
        triggerCol = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plate" && !other.attachedRigidbody.isKinematic && !filled)
        {
            if (other.TryGetComponent(out plate))
            {
                other.attachedRigidbody.isKinematic = true;
                _filled = true;
                onFilled?.Invoke();
                StartCoroutine(Snap(other));
            }
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

    public void PushPlate()
    {
        StartCoroutine(PushPlateRoutine());
    }

    IEnumerator PushPlateRoutine()
    {
        if (!filled || !triggerCol.enabled)
        {
            yield break;
        }
        triggerCol.enabled = false;
        var rigid = plate.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        rigid.AddForce(transform.forward * 100);
        plate = null;
        _filled = false;
        onEmptied?.Invoke();
        yield return new WaitForSeconds(1);
        triggerCol.enabled = true;
    }
}
