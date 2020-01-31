using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public float movementSpeed = 5;
    float yaw;
    float pitch;
    Vector3 movementAxis;
    CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, transform.forward);
        controller.Move(rot * movementAxis * movementSpeed * Time.deltaTime);
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
