using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR;

public class MenuPress : MonoBehaviour
{
    [Header("GameObject")]
    public Transform playerCamera; // Get GameObject of the player
    public Transform menuPos; // menu position and stor for the player moves to it

    //[Header("Menu Position")]

    private float savePosX;
    private float savePosY;
    private float savePosZ;

    private Vector3 initialPos; // Get the player's location it was originally was before the button press
    private float bumper = -42;
    private Quaternion initialRotation; // Get the player's rotation it originally was
    private bool inMenu = false; // bringing up the menu when the button is pressed

    // Start is called at the very start of the game

    private void Start()
    {
        hideMenu();
        initialRotation = playerCamera.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Keyboard debug button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleMenu();
        }

        /*
         * Joystick1Button 6 is the hamburger button on the left controller and if it si Joystick2Button 6 it be the Start button
         * JoystickButton 0 and 1 is A and B respectively
         * JoystickButton 2 and 3 is X and y respectively
         * JoystickButton 4 and 5 is the trigger buttons 1 on top and 2 on bottom of the controller
         */


        // Controler VR headset press to move the camera to the main menu or go back to origin
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            toggleMenu();
        }
    }

    public void toggleMenu()
    {
        if(!inMenu)
        {
            unHideMenu();
            initialPos = playerCamera.position;
            initialRotation = playerCamera.rotation;

            playerCamera.position = new Vector3(menuPos.position.x, menuPos.position.y, menuPos.position.z + bumper);
            playerCamera.rotation = menuPos.rotation;
        }

        else
        {
            playerCamera.position = initialPos;
            playerCamera.rotation = initialRotation;
            hideMenu();
        }

        inMenu = !inMenu;
    }

    public void hideMenu()
    {
        savePosX = menuPos.position.x;
        savePosZ = menuPos.position.z;
        savePosY = menuPos.position.y;

        menuPos.position = new Vector3(0,-120,0);
    }

    public void unHideMenu()
    {
        menuPos.position = new Vector3(savePosX, savePosY, savePosZ);
    }
}
