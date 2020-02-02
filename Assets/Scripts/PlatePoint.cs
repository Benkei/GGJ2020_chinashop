using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatePoint : MonoBehaviour
{
    public string plateType;
    public PlateBrain plate;
    public Transform plateSocket;
    public bool filled => plate != null;

    public UnityEvent onFilled;
    public UnityEvent onEmptied;
    Collider triggerCol;

    void Start()
    {
        triggerCol = GetComponent<Collider>();
        if (plate)
        {
            plate.onBroke.AddListener(OnBreak);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigggerr");
        if (other.tag == "Teller" && !other.attachedRigidbody.isKinematic && !filled)
        {
            if (other.TryGetComponent(out plate))
            {
                if (!plate.BaseModel.activeSelf || plate.type != plateType)
                {
                    plate = null;
                    return;
                }

                plate.enabled = false;
                other.attachedRigidbody.isKinematic = true;
                plate.ResetModel();
                other.tag = "Untagged";
                plate.onBroke.AddListener(OnBreak);
                StartCoroutine(Snap(other));
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
            t += Time.deltaTime * 3;
            yield return null;
        }
        other.transform.position = plateSocket.position;
        other.transform.rotation = plateSocket.rotation;
        plate.enabled = true;
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
        var p = plate;
        plate = null;
        triggerCol.enabled = false;
        p.enabled = true;
        var rigid = p.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        rigid.AddForce(transform.forward * 200);
        rigid.AddTorque(Random.onUnitSphere, ForceMode.Impulse);
        p.gameObject.tag = "Teller";
        p.onBroke.RemoveAllListeners();
        var col = p.GetComponent<Collider>();
        col.isTrigger = true;
        
        onEmptied?.Invoke();
        yield return new WaitForSeconds(0.2f);
        col.isTrigger = false;
        yield return new WaitForSeconds(1);
        triggerCol.enabled = true;
    }

    void OnBreak()
    {
        var rigid = plate.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        plate.onBroke.RemoveAllListeners();
        onEmptied?.Invoke();
        plate = null;
    }
}
