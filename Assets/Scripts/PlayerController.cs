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
    Rigidbody grabbed = null;
    Vector3 grabStartPoint;
    Vector3 position;
    Vector3 positionneu;
    float grabbedTime;
    public AudioSource Step;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, transform.forward);
        controller.Move(rot * new Vector3(movementAxis.x, -gravity, movementAxis.y) * movementSpeed * Time.deltaTime);
        positionneu = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (grabbed)
        {
            grabbed.transform.position = SmoothGrab(grabStartPoint, grabPoint.position, Mathf.Min(1, Time.time - grabbedTime));
            grabbed.transform.rotation = Quaternion.Slerp(grabbed.transform.rotation, grabPoint.transform.rotation, Time.deltaTime * 10);
        }
        if (position != positionneu && Step.isPlaying == false)
        {
            Step.Play();
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
    const string plateTag = "Teller";

    void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            if (grabbed)
            {
                return;
            }
            if (Physics.Raycast(
               camera.ScreenPointToRay(new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2)),
               out var hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide))
            {
                Debug.Log($"Ray: {hit.collider.name} {hit}");
                if (hit.collider.CompareTag(plateTag))
                {
              var repair = hit.rigidbody.GetComponentInParent<RepairPlateBrain>();

               repair.StartRepair(hit.point);

               var root = hit.rigidbody.GetComponentInParent<PlateBrain>();
                    root.enabled = false;
                grabbed = root.GetComponent<Rigidbody>();
                grabbed.isKinematic = true;
                grabStartPoint = root.transform.position;
                grabbedTime = Time.time;

                }
            }
        }
        else
        {
            if (grabbed)
            {
                grabbed.isKinematic = false;
                grabbed.GetComponent<PlateBrain>().enabled = true;
                if (Time.time - grabbedTime > 1)
                {
                    grabbed.AddForce(cameraTransform.forward * 1000);
                }
            }
            grabbed = null;
        }
    }

    static Vector3 SmoothGrab(Vector3 start, Vector3 target, float t)
    {
        return Vector3.Lerp(start, target, EaseInOutElastic(t));
    }

    static float EaseInOutElastic(float k)
    {
        if ((k *= 2f) < 1f) return -0.5f * Mathf.Pow(2f, 10f * (k -= 1f)) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f);
        return Mathf.Pow(2f, -10f * (k -= 1f)) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f) * 0.5f + 1f;
    }

}
