using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    [Header("Rotating Object and time")]
    public Transform sunOrbit;

    // Solstices dates
    public DateTime summerSolstice;
    public DateTime winterSolstice;

    public float dayDuration = 24f; // Real-time hours for a full day in the game

    float sunAngle;
    float gameObjectAngle = 360f;
    float angle;

    public float secDays = 86400; // 24 hours in seconds


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Calculate the current time of day
        sunAngle = System.Math.Abs((float)(DateTime.Now.TimeOfDay.TotalSeconds % secDays) / secDays);

        //sunAngle = System.Math.Abs(sunAngle);

        // Adjust sun's position
        sunOrbit.transform.rotation = Quaternion.Euler(new Vector3(sunAngle * gameObjectAngle - 90f, 0, 0));

        

        /*// Calculate the rotation angle based on real-world time.
        sunAngle = RotateSun();

        // Apply the rotation to the directional light.
        sunOrbit.rotation = Quaternion.Euler(sunAngle, 0f, 0f);*/
    }

    private float RotateSun()
    {

        // Calculate the angle based on the current time.
        angle = (float)(DateTime.Now.Hour * gameObjectAngle / dayDuration);

        return angle;
    }
}
