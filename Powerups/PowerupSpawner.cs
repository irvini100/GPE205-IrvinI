using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float spawnDelay;
    public float nextSpawnTime;
    private Transform tf;
    private GameObject spawnedPickup;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        //If it is there and nothing spawns
        if (spawnedPickup == null)
        {

            //And it is time to spawn a pickup
            if (Time.time > nextSpawnTime)
            {
                //Spawn it and set the next time
                Instantiate(pickupPrefab, transform.position, Quaternion.identity);
                nextSpawnTime += Time.time + spawnDelay;
            }
        }
        else
        {
            //Otherwise, the object still exists, so postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
