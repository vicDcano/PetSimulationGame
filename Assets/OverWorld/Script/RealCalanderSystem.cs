using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Text
using System; // Required for DateTime
using TMPro;

public class RealCalanderSystem : MonoBehaviour
{
    public TMP_Text dateTimeText;

    void Update()
    {
        // Get the current date and time
        DateTime now = DateTime.Now;

        // Format the date and time as a string (you can customize the format)
        string dateTimeString = now.ToString("dd/MM/yyyy\n\nhh:mm tt");

        // Update the UI Text component with the formatted date and time
        dateTimeText.text = dateTimeString;
    }
}
