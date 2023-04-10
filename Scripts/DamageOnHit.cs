using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone;
    public Pawn owner;
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
        //Get the health component from the Game object that has the Collider that we are overlapping.
        Health otherHealth = other.gameObject.GetComponent<Health>();
        //Only damage if it has a Health component.
        if (otherHealth != null)
        {
            //Do damage
            otherHealth.TakeDamage(damageDone, owner);
        }

        //Destroy ourselves, whether we did damage or not
        Destroy(gameObject);
    }
}
