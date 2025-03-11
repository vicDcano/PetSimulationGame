using System.Collections;
using UnityEngine;

public class SkyBoxNightCycle : MonoBehaviour
{
    [Header("SkyBox Material")]
    [SerializeField] private Material skyboxMaterial; // Assign your skybox material here

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 10f; // Time for the transition from 0 to 1

    private float transitionValue = 0f;
    private bool isTransitioning = false;

    void Start()
    {
        if (skyboxMaterial == null)
        {
            Debug.LogError("Skybox Material is not assigned!");
            return;
        }

        // Start the transition loop
        StartCoroutine(TransitionLoop());
    }

    private IEnumerator TransitionLoop()
    {
        while (true) // Infinite loop for continuous transitions
        {
            // Transition from 0 to 1
            yield return StartCoroutine(LerpTransition(0f, 1f, transitionDuration));

            // Transition from 1 to 0
            yield return StartCoroutine(LerpTransition(1f, 0f, transitionDuration));
        }
    }

    private IEnumerator LerpTransition(float start, float end, float time)
    {
        float elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / time);
            transitionValue = Mathf.Lerp(start, end, t);

            // Update the skybox material's transition value
            skyboxMaterial.SetFloat("_CubemapTransition", transitionValue);
            //Debug.Log($"Transition Value: {transitionValue}");

            yield return null;
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxNightCycle : MonoBehaviour
{
    [Header("SkyBox Texture")]
    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxSunset;

    [SerializeField] private Gradient gradientNightToSunrise;
    [SerializeField] private Gradient gradientSunriseToDay;
    [SerializeField] private Gradient gradientDayToSunset;
    [SerializeField] private Gradient gradientSunsetToNight;

    [SerializeField] private Light globalLight;

    private int hours;
    private int minutes;
    private int seconds;
    private int days;

    private float tempSeconds;

    public int Minutes { get { return minutes; } set { minutes = value; onMinutesChange(value); } }
    public int Hours { get { return hours; } set { hours = value; onHoursChange(value); } }
    public int Days { get { return days; } set { minutes = days; } }

    // Update is called once per frame
    void Update()
    {
        tempSeconds = Time.deltaTime;

        if(tempSeconds >= 1 ) 
        {
            minutes += 1;
            tempSeconds = 0;
        }
    }

    private void onMinutesChange(int value)
    {
        globalLight.transform.Rotate(Vector3.up, (1f / 1440f) * 360f, Space.World);
        if(value >= 60)
        {
            Hours++;
            minutes = 0;
        }

        if(Hours >= 24)
        {
            Hours = 0;
            Days++;
        }
    }

    private void onHoursChange(int value)
    {
        if(value == 6)
        {
            StartCoroutine(LerpSkyBox(skyboxNight, skyboxSunrise, 10f));
            StartCoroutine(LerpLight(gradientNightToSunrise, 10f));
        }

        else if(value == 8) 
        {
            StartCoroutine(LerpSkyBox(skyboxSunrise, skyboxDay, 10f));
            StartCoroutine(LerpLight(gradientSunriseToDay, 10f));
        }

        else if (value == 18)
        {
            StartCoroutine(LerpSkyBox(skyboxDay, skyboxSunset, 10f));
            StartCoroutine(LerpLight(gradientDayToSunset, 10f));
        }

        else if((value == 22))
        {
            StartCoroutine(LerpSkyBox(skyboxSunset, skyboxNight, 10f));
            StartCoroutine(LerpLight(gradientSunsetToNight, 10f));
        }
    }

    private IEnumerator LerpSkyBox(Texture2D a, Texture2D b, float time)
    {
        RenderSettings.skybox.SetTexture("_Cubemap", a);
        RenderSettings.skybox.SetTexture("_Cubemap Blend", b);
        RenderSettings.skybox.SetFloat("_Cubemap Transition", 0);
        
        for(float i = 0; i < time; i += Time.deltaTime)
        {
            RenderSettings.skybox.SetFloat("_Cubemap Transition", i / time);
            yield return null;
        }

        RenderSettings.skybox.SetTexture("_Cubemap", b);
    }

    private IEnumerator LerpLight(Gradient lightGradient, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            globalLight.color = lightGradient.Evaluate(i/time);
            RenderSettings.fogColor = globalLight.color;
            yield return null;
        }
    }
}
*/