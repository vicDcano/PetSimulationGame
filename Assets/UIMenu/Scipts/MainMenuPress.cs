using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPress : MonoBehaviour
{
    private MenuStateMachine menuStateMachine;

    [Header("GameObject")]
    public Transform playerCamera; // Get GameObject of the player
    public Transform menuPos; // menu position and stor for the player moves to it
    public GameObject menu;

    //[Header("Menu Position")]

    private Vector3 initialPos; // Get the player's location it was originally was before the button press
    
    private float bumper = -42;
    private Quaternion initialRotation; // Get the player's rotation it originally was

    private bool inMenu = false; // bringing up the menu when the button is pressed

    // Start is called at the very start of the game

    private void Start()
    {
        hideMenu();
        initialRotation = playerCamera.rotation;

        menuStateMachine = GetComponent<MenuStateMachine>();
    }

    public void ToggleMenu()
    {

        if(menuStateMachine.CurrentGameState == GameState.InGame)
        {
            Debug.Log("Closing Main Menu");

            unHideMenu();
            initialPos = playerCamera.position;
            initialRotation = playerCamera.rotation;

            playerCamera.position = new Vector3(menuPos.position.x, menuPos.position.y, menuPos.position.z + bumper);
            playerCamera.rotation = menuPos.rotation;

            // Change to Main Menu state
            menuStateMachine.GameStateChange(GameState.MainMenu);
            menuStateMachine.StateChange(MenuState.MainMenu);
        }

        else if (menuStateMachine.CurrentGameState == GameState.MainMenu)
        {
            Debug.Log("Closing Main Menu");

            playerCamera.position = initialPos;
            playerCamera.rotation = initialRotation;
            hideMenu();

            // Change to Main Menu state
            menuStateMachine.GameStateChange(GameState.InGame);
            menuStateMachine.StateChange(MenuState.None);
        }

    }

    public void hideMenu()
    {
        if (menu != null)
        {
            menu.SetActive(false);
        }

        menuPos.position = new Vector3(0, -120, 0);
    }

    public void unHideMenu()
    {
        if (menu != null)
        {
            menu.SetActive(true);
        }
    }
}
