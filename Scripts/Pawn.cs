using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    
    //Variable for move speed
    public float moveSpeed;
    //Variable for turn speed
    public float turnSpeed;
    //Variable to hold our mover
    public Mover mover;
    //Variable for rate of fire
    public float fireRate;
    public float currentHealth;
    public float maxHealth;
    //Variable to hold our shooter
    public Shooter shooter;
    //Variable for our shellPrefab
    public GameObject Bullet;
    //Variable for our firing force
    public float fireForce;
    //Variable for our damage done
    public float damageDone;
    //Variable for how long our bullets survive if they don't collide
    public float shellLifeSpan;
 


    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();
    public abstract void RotateTowards(Vector3 targetPosition);
}
