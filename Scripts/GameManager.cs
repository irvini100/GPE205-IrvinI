using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform playerSpawnTraansform;
    //Prefabs
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    public static GameManager instance;
    public List<PlayerController> players;
    public List<TankPawn> tankPawn;
    /*public List<AIController> aiController;*/
    //Game States
   /* public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;*/
    

    //Awake is called when the object is first created - before even Start can run!
    private void Awake()
    {
        //If the instance doesn't ecxist yet...
        if (instance == null )
        {
            //This is the instance
            instance = this;
            //Don't destroy it if we load a new scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Otherwise their is already in instance, so destroy this gameObject.
            Destroy(gameObject);
        }
        players = new List<PlayerController> ();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Temp Code - for now, we spawn player as soon as the GameManager starts
        SpawnPlayer();
        /*aiController = new List<AIController>();*/
        tankPawn = new List<TankPawn>();

       /* ActivateGameplayScreen();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
        //Spawn the player controller at (0,0,0) with no rotation
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        //Spawn the pawn and connect it to the controller
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTraansform.position, playerSpawnTraansform.rotation) as GameObject;
       

        //Get the player controller component and pawn component
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        //Hook them up!
        newController.pawn = newPawn;
        newPawn.controller = newController;
    }
    
     /* private void DeactivateAllStates()
    {
        //Deactivate all game states
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
    }

    private void ActivateTitleScreen()
    {
        //Deactivate all states
        DeactivateAllStates();
        //Activate the title screen
        TitleScreenStateObject.SetActive(true);
        //Do whatever needs to be done when the title screen starts.
        //For this game, their is nothing to do, but it may be different for your game.

    }
    
    private void ActivateMainMenu()
    {
        //Deactivate all states
        DeactivateAllStates ();
        MainMenuStateObject.SetActive(true);
    }

    private void ActivateOptionsScreen()
    {
        //Deactivate all states
        DeactivateAllStates();
        OptionsScreenStateObject.SetActive(true);
    }

    private void ActivateCreditsScreen()
    {
        DeactivateAllStates();
        CreditsScreenStateObject.SetActive(true);
    }

    private void ActivateGameplayScreen()
    {
        DeactivateAllStates();
        GameplayStateObject.SetActive(true);
    }

    private void ActivateGameoverScreen()
    {
        DeactivateAllStates();
        GameOverScreenStateObject.SetActive(true) ;
    }

    public void TitleScreenButton()
    {
        if (GameManager.instance != null)

        {
            GameManager.instance.ActivateMainMenu();
        }
    }

    public void ReturnToMainMenuButton2()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateMainMenu();
        }

    }
    public void ActivateGameplayButton()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateGameplayScreen();
        }
    }

    public void ActivateOptionsScreenButton()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateOptionsScreen();
        }
    }

    public void ActivateCreditsScreenButton()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateCreditsScreen();
        }
    }*/
}
