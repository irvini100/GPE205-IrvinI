using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public HealthPowerup powerup;
    public AudioClip GetAudio;
    public GameObject hpowerup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        //Variable to store other object's PowerupController-if it has one.
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        //If the other object has a PowerupController
        if (powerupManager != null )
        {
            //Add the powerup
            powerupManager.Add(powerup);
            Debug.Log("Added Health!");

            if (GetAudio != null )
            {
                AudioSource.PlayClipAtPoint(GetAudio, hpowerup.transform.position);
            }


            //Destroy this pickup
            Destroy(gameObject);
        }
    }
}
