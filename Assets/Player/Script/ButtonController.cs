using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private MenuStateMachine menuState;
    [SerializeField] private MainMenuPress mainMenuPress;

    public DungeonExploring caveEntrance;

    private InputDevice leftController;
    private InputDevice rightController;

    private void Start()
    {
        GetDevices();
        /*menuState = GetComponent<MenuStateMachine>();
        InGgame();*/
        /*needsMenu = GetComponent<NeedsMenuToggle>();
        mainMenu = GetComponent<MainMenuPress>();*/
    }

    private void Update()
    {
        if (!leftController.isValid || !rightController.isValid)
        {
            GetDevices();
        }

        CheckForInput();
    }

    private void GetDevices()
    {
        var leftHandDevices = new List<InputDevice>();
        var rightHandDevices = new List<InputDevice>();

        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);

        if (leftHandDevices.Count > 0)
        {
            leftController = leftHandDevices[0];
        }

        if (rightHandDevices.Count > 0)
        {
            rightController = rightHandDevices[0];
        }
    }

    private void CheckForInput()
    {
        // Check for Menu Button (Left Controller)
        if (leftController.TryGetFeatureValue(CommonUsages.menuButton, out bool menuButtonPressed) && menuButtonPressed)
        {
            Debug.Log("Menu Button Pressed");
            mainMenuPress.ToggleMenu();
        }

        // Check for A Button (Right Controller)
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool aButtonPressed) && aButtonPressed)
        {
            Debug.Log("A Button Pressed");
            menuState.StateChange(MenuState.Status);
        }

        // Check for B Button (Right Controller)
        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool bButtonPressed) && bButtonPressed)
        {
            Debug.Log("B Button Pressed");
            GoToCave(); // Call the method to go to the cave
        }
    }

    private void GoToCave()
    {
        if (caveEntrance != null)
        {
            caveEntrance.StartTimer(); // Start the timer and teleport logic
            Debug.Log("Player is going to the cave.");
        }
        else
        {
            Debug.LogError("Cave Entrance script is not assigned.");
        }
    }

    /*private void Update()
    {
        *//*
         * Joystick1Button 6 is the hamburger button on the left controller and if it si Joystick2Button 6 it be the Start button
         * JoystickButton 0 and 1 is A and B respectively
         * JoystickButton 2 and 3 is X and y respectively
         * JoystickButton 4 and 5 is the trigger buttons 1 on top and 2 on bottom of the controller
         *//*

        if (Input.GetKeyDown(KeyCode.A))
        {
            menuState.StateChange(MenuState.Status);

        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            menuState.StateChange(MenuState.Status);
        }

        // Keyboard debug button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenuPress.ToggleMenu();

        }

        // Controler VR headset press to move the camera to the main menu or go back to origin
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            mainMenuPress.ToggleMenu();

        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            caveEntrance.StartTimer();
        }

        if(Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            caveEntrance.StartTimer();
        }
    }*/

}
