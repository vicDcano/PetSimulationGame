using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR;

public class CameraRestriction : MonoBehaviour
{
    public float minYRotation = -60f; // Minimum allowed Y rotation
    public float maxYRotation = 60f;  // Maximum allowed Y rotation
    public float minXRotation = -30f; // Minimum allowed X rotation
    public float maxXRotation = 30f;  // Maximum allowed X rotation

    private Transform xrCamera;

    void Start()
    {
        // Get the XR camera transform (usually the main camera in VR)
        xrCamera = Camera.main.transform;
    }

    void Update()
    {
        // Get the current rotation of the camera
        Vector3 currentRotation = xrCamera.localEulerAngles;

        // Clamp the X and Y rotation values
        currentRotation.x = ClampAngle(currentRotation.x, minXRotation, maxXRotation);
        currentRotation.y = ClampAngle(currentRotation.y, minYRotation, maxYRotation);

        // Apply the clamped rotation to the camera
        xrCamera.localEulerAngles = currentRotation;
    }

    // Helper function to clamp angles correctly (handles Unity's 0-360 range)
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
