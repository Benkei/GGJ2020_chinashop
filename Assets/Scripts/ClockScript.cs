using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    public GameObject bigPointer;
    public GameObject smallPointer;

    public float startHour = 4;
    public float endHour = 5;

    float minutesPerSecond;

    void Start()
    {
        minutesPerSecond = (endHour - startHour) * 60 / GameplayManager.maxTime;
    }

    void Update()
    {
        //var bigEuler = bigPointer.transform.localEulerAngles;
        //var smallEuler = smallPointer.transform.localEulerAngles;

        //bigEuler.x = Mathf.Repeat(bigEuler.x - Time.deltaTime * 20, 180);
        //smallEuler.x = Mathf.Repeat(smallEuler.x - Time.deltaTime * 20, 180);
        //bigPointer.transform.Rotate(-Time.deltaTime * 20 * 12, 0, 0);
        //smallPointer.transform.Rotate(-Time.deltaTime * 20, 0, 0);

        //bigPointer.transform.localEulerAngles = bigEuler;
        //smallPointer.transform.localEulerAngles = smallEuler;
        var minutes = GameplayManager.currentTime * minutesPerSecond;

        SetMinute(minutes % 60);
        SetHour(startHour + minutes / 60);
    }


    void SetMinute(float x)
    {
        var lx = (1 - x / 60.0f) * 360;
        bigPointer.transform.localEulerAngles = new Vector3(lx, 0, 0);
    }

    void SetHour(float x)
    {
        const float zero = 222.5f;
        var lx = (1 - x / 12.0f) * 360 + zero;
        smallPointer.transform.localEulerAngles = new Vector3(lx, 0, 0);
    }
}
