using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    public TankMover tankMover;
    float speed;
    public Rigidbody rb;
    Vector3 moveVector;
    private float secondsForShot;
    public float shotsPerSecond = 1.0f;
    private object pawn;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        secondsForShot = Time.time + shotsPerSecond;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        float secondsPerShot = 1 / shotsPerSecond;
    }

    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
    }

    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    public override void Shoot()
    {
        shooter.Shoot(Bullet, fireForce, damageDone, shellLifeSpan);
    }

    public override void OpenDoor()
    {
        mapGenerator.GenerateMap();
    }

    public override void Controller(Controller controller)
    {
        controller.pawn = this;
    }
   

    public override void RotateTowards(Vector3 targetPosition)
    {
        //Find the vector to our target
        Vector3 vectorToTarget = targetPosition - transform.position;
        
        //Find the rotation to look down that vector
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        //Rotate closer to that vector, but don't rotate more than our turn speed allows in one frame
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public void IncreaseSpeed(float amount, Pawn source)
    {
        moveSpeed = moveSpeed + amount;
    }

    public void DecreaseSpeed(float amount, Pawn source)
    {
        moveSpeed = moveSpeed - amount;
    }
}
