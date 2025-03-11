using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsMenuToggle : MonoBehaviour
{
    public GameObject needsPanel;

    public bool isActive = true; // Variable to control active state

    public void ToggleObject()
    {
        if(!isActive)
        {
            needsPanel.SetActive(true); // Set active based on new state
        }

        else
        {
            needsPanel.SetActive(false);
        }

        isActive = !isActive;

    }
}
