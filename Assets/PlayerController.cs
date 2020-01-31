using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    float yaw;
    float pitch;
    Vector3 movementAxis;

    void Update()
    {
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, transform.forward);
        transform.position += rot * movementAxis * Time.deltaTime;
    }

    void OnLook(InputValue value)
    {
        var axis = value.Get<Vector2>();
        yaw += axis.x;
        pitch += axis.y * -1;
        pitch = Mathf.Clamp(pitch, -80, 80);
        transform.eulerAngles = new Vector3(0, yaw, 0);
        cameraTransform.localEulerAngles = new Vector3(pitch, 0, 0);
    }

    void OnMove(InputValue value)
    {
        var axis = value.Get<Vector2>();
        movementAxis = new Vector3(axis.x, 0, axis.y);
    }
}
