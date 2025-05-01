using UnityEngine;

public enum GameState
{
    MainMenu,
    InGame
}

public enum MenuState
{
    None,
    MainMenu,
    Status,
    Inventory,
    Shop,
    Settings,
    LifeSpan,
    PopUp
}

public class MenuStateMachine : MonoBehaviour
{
    // Variable for the Enum and set default to None
    private MenuState currentMenuDisplay = MenuState.None;
    private GameState currentGameState = GameState.InGame;

    //Variables of our objects
    public GameObject needsMenu;
    public GameObject inventory;
    public GameObject shop;
    public GameObject settings;
    public GameObject lifeSpan;
    public GameObject popUp;
    public GameObject mainMenuDisp;
    //private MainMenuPress mainMenu;

    public GameState CurrentGameState => currentGameState;

    private void Start()
    {
        /*mainMenu = GetComponent<MainMenuPress>();*/
        DeactivatePanels();
    }

    public void GameStateChange(GameState state)
    {
        currentGameState = state;

        if(currentGameState == GameState.MainMenu) 
        {
            if(currentMenuDisplay == MenuState.Inventory || currentMenuDisplay == MenuState.Status)
            {
                StateChange(MenuState.None);
            }
        }
    }

    public void StateChange(MenuState newState)
    {
        if(currentGameState == GameState.MainMenu && (newState == MenuState.Inventory || newState == MenuState.Status))
        {
            return;
        }

        if (currentMenuDisplay == newState)
        {
            ExitState(currentMenuDisplay);
            currentMenuDisplay = MenuState.None;
        }
        else
        {
            ExitState(currentMenuDisplay);
            currentMenuDisplay = newState;
            StepIntoState(currentMenuDisplay);
        }
    }

    private void StepIntoState(MenuState currentMenuState)
    {
        switch(currentMenuState) 
        {
            case MenuState.None:
                DeactivatePanels();
                break;

            case MenuState.MainMenu:
                //mainMenu.ToggleMenu();
                mainMenuDisp.SetActive(true);
                break;

            case MenuState.Status:
                needsMenu.SetActive(true);
                break;

            case MenuState.Inventory:
                inventory.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                break;

            case MenuState.Shop:
                shop.SetActive(true);
                break;

            case MenuState.Settings:
                settings.SetActive(true);
                break;

            case MenuState.LifeSpan:
                lifeSpan.SetActive(true);
                break;

            case MenuState.PopUp:
                popUp.SetActive(true);
                break;
        }
    }

    private void ExitState(MenuState state) 
    {
        switch (state)
        {
            case MenuState.MainMenu:
                //mainMenu.ToggleMenu();
                mainMenuDisp.SetActive(false);
                break;

            case MenuState.Status:
                needsMenu.SetActive(false);
                break;

            case MenuState.Inventory:
                inventory.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                break;

            case MenuState.Shop:
                shop.SetActive(false);
                break;

            case MenuState.Settings:
                settings.SetActive(false);
                break;

            case MenuState.LifeSpan:
                lifeSpan.SetActive(false);
                break;

            case MenuState.PopUp:
                popUp.SetActive(false);
                break;
        }
    }

    private void DeactivatePanels()
    {
        needsMenu.SetActive(false);
        inventory.SetActive(false);
        shop.SetActive(false);
        settings.SetActive(false);
        lifeSpan.SetActive(false);
        popUp.SetActive(false);
        mainMenuDisp.SetActive(false);
    }
}