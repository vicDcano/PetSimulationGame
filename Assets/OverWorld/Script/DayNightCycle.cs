using System;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time Settings")]
    [Range(0, 1)] public float timeOfDay; // 0 = midnight, 1 = next midnight
    public float dayDurationInMinutes = 24f; // Real-time would be 1440 (24*60)
    public bool useRealTime = false;

    [Header("Skybox Control")]
    public Material skyboxMaterial;
    public float fullDayTime = 6f;    // 6 AM - fully day
    public float fullNightTime = 20f;  // 8 PM - fully night
    public float blendDuration = 2f;   // Hours for dawn/dusk

    [Header("Light Control")]
    public Light directionalLight;
    public float minLightIntensity = 0.1f;
    public float maxLightIntensity = 1f;
    public Color nightLightColor = new Color(0.1f, 0.1f, 0.3f);
    public Color dayLightColor = Color.white;
    public Color sunsetLightColor = new Color(1f, 0.5f, 0.2f);

    [Header("Sunrise/Sunset Settings")]
    public float sunriseOrangePeak = 0.3f; // When orange is strongest in sunrise (0-1)
    public float sunsetOrangePeak = 0.7f;  // When orange is strongest in sunset (0-1)

    void Update()
    {
        UpdateTime();
        UpdateSkybox();
        UpdateLight();
    }

    private void UpdateTime()
    {
        if (useRealTime)
        {
            timeOfDay = (float)System.DateTime.Now.TimeOfDay.TotalHours / 24f;
        }
        else
        {
            timeOfDay += Time.deltaTime / (dayDurationInMinutes * 60f);
            timeOfDay %= 1f;
        }
    }

    private void UpdateSkybox()
    {
        if (!skyboxMaterial) return;

        float currentHour = timeOfDay * 24f;
        float transitionValue = 0f;

        if (currentHour >= fullDayTime && currentHour <= fullNightTime)
        {
            if (currentHour < fullDayTime + blendDuration)
            {
                // Dawn transition (night → day)
                transitionValue = 1f - Mathf.InverseLerp(
                    fullDayTime,
                    fullDayTime + blendDuration,
                    currentHour);
            }
            else if (currentHour > fullNightTime - blendDuration)
            {
                // Dusk transition (day → night)
                transitionValue = Mathf.InverseLerp(
                    fullNightTime - blendDuration,
                    fullNightTime,
                    currentHour);
            }
            // Else full day (transitionValue remains 0)
        }
        else
        {
            // Full night
            transitionValue = 1f;
        }

        skyboxMaterial.SetFloat("_CubemapTransition", transitionValue);
    }

    private void UpdateLight()
    {
        if (!directionalLight) return;

        // Rotate light (same as before)
        float sunAngle = (timeOfDay + 0.25f) * 360f;
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle, -90f, 0f);

        // Get current hour and calculate transition state
        float currentHour = timeOfDay * 24f;
        float lightTransition = 0f;
        bool isDaytime = false;

        if (currentHour >= fullDayTime && currentHour <= fullNightTime)
        {
            if (currentHour < fullDayTime + blendDuration)
            {
                // Dawn transition (night → day)
                lightTransition = 1f - Mathf.InverseLerp(
                    fullDayTime,
                    fullDayTime + blendDuration,
                    currentHour);
            }
            else if (currentHour > fullNightTime - blendDuration)
            {
                // Dusk transition (day → night)
                lightTransition = Mathf.InverseLerp(
                    fullNightTime - blendDuration,
                    fullNightTime,
                    currentHour);
            }
            else
            {
                // Full daytime
                isDaytime = true;
            }
        }

        // Improved color transition logic
        if (isDaytime)
        {
            // Pure daylight
            directionalLight.color = dayLightColor;
            directionalLight.intensity = maxLightIntensity;
        }
        else if (currentHour < fullDayTime + blendDuration)
        {
            // Sunrise: night → orange → white
            float dawnProgress = Mathf.InverseLerp(fullDayTime, fullDayTime + blendDuration, currentHour);
            if (dawnProgress < 0.5f)
            {
                // First half: night → sunset color
                directionalLight.color = Color.Lerp(nightLightColor, sunsetLightColor, dawnProgress * 2f);
            }
            else
            {
                // Second half: sunset → day color
                directionalLight.color = Color.Lerp(sunsetLightColor, dayLightColor, (dawnProgress - 0.5f) * 2f);
            }
            directionalLight.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, dawnProgress);
        }
        else if (currentHour > fullNightTime - blendDuration)
        {
            // Sunset: white → orange → night
            float duskProgress = Mathf.InverseLerp(fullNightTime - blendDuration, fullNightTime, currentHour);
            if (duskProgress < 0.5f)
            {
                // First half: day → sunset color
                directionalLight.color = Color.Lerp(dayLightColor, sunsetLightColor, duskProgress * 2f);
            }
            else
            {
                // Second half: sunset → night color
                directionalLight.color = Color.Lerp(sunsetLightColor, nightLightColor, (duskProgress - 0.5f) * 2f);
            }
            directionalLight.intensity = Mathf.Lerp(maxLightIntensity, minLightIntensity, duskProgress);
        }
        else
        {
            // Full nighttime
            directionalLight.color = nightLightColor;
            directionalLight.intensity = minLightIntensity;
        }
    }

    // For debugging
    void OnGUI()
    {
        float currentHour = timeOfDay * 24f;
        GUI.Label(new Rect(10, 10, 300, 20), $"Time: {Mathf.Floor(currentHour):00}:{Mathf.Floor((currentHour % 1) * 60):00}");
        GUI.Label(new Rect(10, 30, 300, 20), $"Skybox Transition: {skyboxMaterial.GetFloat("_CubemapTransition"):F2}");
        GUI.Label(new Rect(10, 50, 300, 20), $"Light Intensity: {directionalLight.intensity:F2}");
    }
}