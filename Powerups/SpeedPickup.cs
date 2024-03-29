using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public SpeedPowerup speedPowerup;
    public AudioClip AudioClip;
    public GameObject sp;

    //Start is called before the first frame update
    void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {
       
    }

    public void OnTriggerEnter(Collider other)
    {
        //Variable to store other object's PowerupController-if it has one
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        //If the other object has a PowerupController
        if (powerupManager != null )
        {
            //Add the powerup
            powerupManager.Add(speedPowerup);
            Debug.Log("Added speed!");

            if (AudioClip != null)
            {
                AudioSource.PlayClipAtPoint(AudioClip, sp.transform.position);
            }
            //Destroy this object
            Destroy(gameObject);
        }
    }

}

