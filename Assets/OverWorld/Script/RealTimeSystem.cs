using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RealTimeSystem : MonoBehaviour
{
    [Header("Rotating Object and time")]
    public Transform sunOrbit;

    // Solstices dates
    public DateTime summerSolstice;
    public DateTime winterSolstice;

    public float dayDuration = 24f; // Real-time hours for a full day in the game

    float sunAngle;
    float gameObjectAngle = 360f;

    public float secDays = 86400; // 24 hours in seconds+

    // Skyboxes
    [SerializeField]
    public Material blend;

    // Update is called once per frame
    void Update()
    {

        // Calculate the current time of day
        sunAngle = System.Math.Abs((float)(DateTime.Now.TimeOfDay.TotalSeconds % secDays) / secDays);

        // Calculate and returns an angle to use
        float angle = fullRotation(sunAngle, gameObjectAngle);

        // Adjust sun's position according to the angle found
       sunOrbit.transform.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));

        ///switches between sky boxes
        updateSkybox(angle);
    }

    public float fullRotation(float sunAngle, float gameObjectAngle)
    {
        float temp = sunAngle * gameObjectAngle - 90f;

        if(temp < 0)
        {
            temp = temp += 360; ;
        }

        return temp;
    }

    public void updateSkybox(float angle)
    {
        if (angle >= 330f && angle < 350f)
        {
        }

        else if (angle >= 350f || angle < 10f)
        {
        }

        else if (angle >= 10f && angle < 170f)
        {
        }

        else if (angle >= 170f && angle < 180f)
        {
        }

        else if (angle >= 180f && angle < 200f)
        {
        }

        else if (angle >= 200f && angle < 330f)
        {
        }
    }
}
