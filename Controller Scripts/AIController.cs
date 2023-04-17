using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    private float lastStateChangeTime;
    public GameObject target;
    public enum AIState { Idle, Guard, Chase, Flee, Patrol, Attack, Scan, BackToPost };
    public AIState currentState;
    public Transform[] waypoints;
    public float waypointStopDistance;
    private int currentWaypoint = 0;
    public float fleeDistance;
    public float hearingDistance;
    public float fieldOfView;
    // Start is called before the first frame update
    public override void Start()
    {
        //Run the parent (base) Start
        base.Start();

        ChangeState(AIState.Idle);
    }

    // Update is called once per frame
    public override void Update()
    {
        //Run the parent (base) Update
        base.Update();

        //Make decisions
        MakeDecisions();
    }

    protected virtual void DoChaseState()
    {
        Seek(target);
    }

    protected virtual void DoIdleState()
    {
     //Do nothing
    }

    public void MakeDecisions()
    {
        Debug.Log("Making Decisions");
        switch (currentState)
        {
            case AIState.Idle:
                //Do work
                DoIdleState();
                //Check for transitions
                if (IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Chase:
                //Do work
                DoChaseState();
                //Check for transitions
                if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Idle);
                }
                else if (IsDistanceLessThan(target, 10))
                { 
                    ChangeState(AIState.Attack);
                }
                break;
            case AIState.Attack:
                //Do work
                DoAttackState();
                //Check for transitions
                if(IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Idle);
                }
                break;
        }
    }

    public virtual void ChangeState(AIState newState)
    {
        //Change the current state
        currentState = newState;
        //Save the time when we changed states
        lastStateChangeTime = Time.time;
    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        Debug.Log("Pawn" + pawn.name);
        Debug.Log(message: "Target" + target.name);
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Seek(Vector3 targetPosition)
    {
        //Rotate towards the function
        pawn.RotateTowards(targetPosition);

        //Move forward
        pawn.MoveForward();
    }

    public void Seek(Transform targetTransform)
    {
        //Seek the position of our target Transform
        Seek(targetTransform.position);
    }

    public void Seek(Pawn targetPawn)
    {
        //Seek the pawn's transform
        Seek(targetPawn.transform);
    }

    public void Seek(GameObject target)
    {
        pawn.RotateTowards(target.transform.position);
        //Move forward
        pawn.MoveForward();
    }

    public void Shoot()
    {
        //Tell the pawn to shoot
        pawn.Shoot();
    }

    protected virtual void DoAttackState()
    {
        //Chase
        Seek(target);
        //Shoot
        Shoot();
    }

    protected void Flee()
    {
        //Find the vector to our target
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;

        //Find the vector away from our target by multiplying by -1
        Vector3 vectorAwayFromTarget = -vectorToTarget;

        //Find the vector we would travel down in order to flee
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;

        //Seek the point that is "fleeVector" away from our current position
        Seek(pawn.transform.position + fleeVector);

        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
        float percentOfFleeDistance = targetDistance / fleeDistance;
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
    }

    protected void Patrol()
    {
        //If we have enough waypoints in our list to move to a current waypoint
        if (waypoints.Length > currentWaypoint)
        {
            //Then seek that waypoint
            Seek(waypoints[currentWaypoint]);
            //If we are close enough, then increment to the next waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }
        } else
        {
            RestartPatrol();
        }
    }

    protected void RestartPatrol()
    {
        //Set the index to 0
        currentWaypoint = 0;
    }

    public void TargetPlayerOne()
    {
        //If the GameManager exists
        if (GameManager.instance != null)
        {
            //And the array of players exists
            if (GameManager.instance.players != null)
            {
                //And their are players in it
                if (GameManager.instance.players.Count > 0)
                {
                    //Then target the gameObject of the pawn of the first player controller in the list
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }

    protected bool IsHasTarget()
    {
        //Return true if we have a target, false if we don't
        return (target != null);
    }

    protected void TargetNearestTank()
    {
        //Get a list of all the tanks (pawns)
        Pawn[] allTanks = FindObjectsOfType<Pawn>();

        //Assume that the first tank is closest
        Pawn closestTank = allTanks[0];
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        //Iterate through them one at a time
        foreach (Pawn tank in allTanks)
        {
            //If this one is closer than the closest
            if (Vector3.Distance(pawn.transform.position, tank.transform.position) <= closestTankDistance)
            {
                //It is the closest
                closestTank = tank;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
            }
        }

        //Target the closest tank
        target = closestTank.gameObject;
    }

    public bool CanHear(GameObject target)
    {
        //Get the target's NoiseMaker
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
        //If they don't have one, they can't make noise, so return false
        if (noiseMaker != null)
        {
            return false;
        }

        //If they are making a noise, add the volumeDistance in the noisemaker to the hearingdistance of this AI
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;

        //If the distance between our pawn and target is closer than this...
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            //...then we can hear the target
            return true;
        }
        else
        {
            //Otherwise, we are too far to hear them
            return false;
        }
    }

    public bool CanSee(GameObject target)
    {
        //Find the vector from the agent to the target
        Vector3 agentToTargetVector = target.transform.position - transform.position;

        //Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);

        //If that angle is less than our field of view
        if (angleToTarget < fieldOfView)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
