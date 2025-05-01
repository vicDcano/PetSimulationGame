using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private MenuStateMachine menuState;
    [SerializeField] private MainMenuPress mainMenuPress;
    [SerializeField] private InventoryManager inventoryManager;

    public DungeonExploring caveEntrance;

    public InputActionReference buttonY = null;
    public InputActionReference buttonX = null;
    public InputActionReference menuButton = null;
    public InputActionReference confirmButton = null;

    private void Awake()
    {
        buttonY.action.started += ToggleNeeds;
        buttonX.action.started += ToggleInventory;
        menuButton.action.started += ToggleMenu;
        confirmButton.action.started += confirmPressed;

    }

    private void OnDestroy()
    {
        buttonY.action.started -= ToggleNeeds;
        buttonX.action.started -= ToggleInventory;
        menuButton.action.started -= ToggleMenu;
        confirmButton.action.started -= confirmPressed;

    }

    private void ToggleNeeds(InputAction.CallbackContext context)
    {
        /*bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);*/
        menuState.StateChange(MenuState.Status);
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        /*bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);*/

        mainMenuPress.ToggleMenu();
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        menuState.StateChange(MenuState.Inventory);
    }

    public void confirmPressed(InputAction.CallbackContext context)
    {
        // Only process if in the MainMenu state
        if (menuState.CurrentGameState == GameState.MainMenu)
        {
            // Check if a UI button is currently selected (hovered)
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                // Simulate a button click
                ExecuteEvents.Execute(
                    EventSystem.current.currentSelectedGameObject,
                    new BaseEventData(EventSystem.current),
                    ExecuteEvents.submitHandler
                );
                Debug.Log("Confirm button pressed on UI element!");
            }
        }
    }

    public void OnShopButtonPressed()
    {
        if (menuState.CurrentGameState == GameState.MainMenu)
        {
            menuState.StateChange(MenuState.Shop);
        }
    }

    public void OnSettingsButtonPressed()
    {
        if (menuState.CurrentGameState == GameState.MainMenu)
        {
            menuState.StateChange(MenuState.Settings);
        }
    }



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
