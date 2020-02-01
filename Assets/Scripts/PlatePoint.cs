using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatePoint : MonoBehaviour
{
    public PlateBrain plate;
    public Transform plateSocket;
    [SerializeField]
    bool _filled = false;
    public bool filled => _filled;

    public UnityEvent onFilled;
    public UnityEvent onEmptied;
    Collider triggerCol;

    void Start()
    {
        triggerCol = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigggerr");
        if (other.tag == "Teller" && !other.attachedRigidbody.isKinematic && !filled)
        {
            if (other.TryGetComponent(out plate) && plate.BaseModel.activeSelf)
            {
                other.attachedRigidbody.isKinematic = true;
                other.tag = "Untagged";
                StartCoroutine(Snap(other));
                _filled = true;
                onFilled?.Invoke();
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
            other.transform.position = Vector3.Lerp(start, plateSocket.position, t);
            other.transform.rotation = Quaternion.Slerp(startRot, plateSocket.rotation, t);
            t += Time.deltaTime;
            yield return null;
        }
        other.transform.position = plateSocket.position;
        other.transform.rotation = plateSocket.rotation;
    }

    public void PushPlate()
    {
        StartCoroutine(PushPlateRoutine());
    }

    IEnumerator PushPlateRoutine()
    {
        Debug.Log("Routine!");
        if (!filled || !triggerCol.enabled)
        {
            yield break;
        }
        _filled = false;
        triggerCol.enabled = false;
        var rigid = plate.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        rigid.AddForce(transform.forward * 200);
        plate.gameObject.tag = "Teller";
        var col = plate.GetComponent<Collider>();
        col.isTrigger = true;
        plate = null;
        Debug.Log("Set tag");
        onEmptied?.Invoke();
        yield return new WaitForSeconds(0.2f);
        col.isTrigger = false;
        yield return new WaitForSeconds(1);
        triggerCol.enabled = true;
    }
}
