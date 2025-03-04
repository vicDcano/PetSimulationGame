using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR;
using UnityEngine.XR;

public class CameraRestriction : MonoBehaviour
{
    public Transform petTransform; // Reference to the pet or the environment center
    public float maxYRotation = 45f; // Adjust this value to limit rotation
    public float maxXPosition = 0.1f; // Adjust this value to limit up/down movement
    public float maxZPosition = 0.1f; // Adjust this value to limit side-to-side movement

    private Vector3 initialPosition; // Store the initial position of the camera

    private void Start()
    {
        // Store the initial position of the camera
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Get the headset's rotation
        Quaternion headsetRotation = InputTracking.GetLocalRotation(XRNode.Head);
        float yRotation = headsetRotation.eulerAngles.y;

        // Normalize the rotation to -180 to 180
        if (yRotation > 180) yRotation -= 360;

        // Clamp the Y rotation
        yRotation = Mathf.Clamp(yRotation, -maxYRotation, maxYRotation);

        // Calculate the target rotation based on the pet's position
        Vector3 directionToPet = petTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPet, Vector3.up);

        // Apply the clamped rotation to the target rotation
        Quaternion finalRotation = Quaternion.Euler(0, yRotation, 0) * targetRotation;

        // Apply the final rotation to the camera
        transform.rotation = finalRotation;

        // Get the headset's position
        Vector3 headsetPosition = InputTracking.GetLocalPosition(XRNode.Head);

        // Clamp the X and Z positions
        float clampedX = Mathf.Clamp(headsetPosition.x, -maxXPosition, maxXPosition);
        float clampedZ = Mathf.Clamp(headsetPosition.z, -maxZPosition, maxZPosition);

        // Apply the clamped position to the camera
        transform.position = initialPosition + new Vector3(clampedX, 0, clampedZ);
    }
}
