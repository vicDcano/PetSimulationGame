using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private MenuStateMachine menuState;
    [SerializeField] private MainMenuPress mainMenuPress;

    public DungeonExploring caveEntrance;

   /* public InputAction controllerPress;
    public InputActionReference ButtonX;
    public InputActionReference MenuButton;

    private void Start()
    {
        ButtonX.action.performed += buttonX;
    }

    private void OnMouseUpAsButton(InputAction.CallbackContext.callback)
    {
        menuState.StateChange(MenuState.Status);
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

    private void OnDestroy()
    {
        ButtonX.action.performed -= buttonX;
    }
*/
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
