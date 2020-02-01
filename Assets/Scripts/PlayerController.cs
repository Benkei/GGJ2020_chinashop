using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public Camera camera;
    public Transform grabPoint;
    public float movementSpeed = 5;
    public float gravity = 20.0f;
    float yaw;
    float pitch;
    Vector3 movementAxis;
    CharacterController controller;
    Transform grabbed = null;
    Vector3 grabStartPoint;
    float grabbedTime;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, transform.forward);
        controller.Move(rot * new Vector3(movementAxis.x, -gravity, movementAxis.y) * movementSpeed * Time.deltaTime);
        if (grabbed)
        {
            grabbed.transform.position = SmoothGrab(grabStartPoint, grabPoint.position, Mathf.Min(1, Time.time - grabbedTime));
            grabbed.transform.rotation = Quaternion.Slerp(grabbed.transform.rotation, grabPoint.transform.rotation, Time.deltaTime * 10);
        }
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
        movementAxis = value.Get<Vector2>();
    }
    const string plateTag = "PlatePiece";

    void OnFire(InputValue value)
    {
        if (grabbed)
        {
            return;
        }
        if (Physics.Raycast(camera.ScreenPointToRay(new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2)), out var hit))
        {
            Debug.Log(hit);
            if (hit.collider.CompareTag(plateTag) || true)
            {
                grabbed = hit.transform;
                grabStartPoint = hit.transform.position;
                grabbedTime = Time.time;
            }
        }
    }

    void OnAltFire(InputValue value)
    {
        grabbed = null;
    }

    static Vector3 SmoothGrab(Vector3 start, Vector3 target, float t)
    {
        return Vector3.Lerp(start, target, EaseInOutElastic(t));
        if (t < 0.8f)
        {
            return Vector3.Lerp(start, target, Mathf.SmoothStep(0, 0.8f, t * 1 / 0.8f));
        }

        return Vector3.Lerp(start, target, t);
    }

    static float EaseInOutElastic(float k)
    {
        if ((k *= 2f) < 1f) return -0.5f * Mathf.Pow(2f, 10f * (k -= 1f)) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f);
        return Mathf.Pow(2f, -10f * (k -= 1f)) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f) * 0.5f + 1f;
    }

}
