using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerSpawnTraansform;
    //Prefabs
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    public static GameManager instance;
    public List<PlayerController> players;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        //Temp Code - for now, we spawn player as soon as the GameManager starts
        SpawnPlayer();
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
    }
}
