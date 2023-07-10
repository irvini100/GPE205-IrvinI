using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class SpeedPowerup : Powerup
{
    public float speedToAdd;
    public float speedToRemove;
    public override void Apply(PowerupManager target)
    {
        //Apply speed changes
        TankPawn tankPawn = target.GetComponent<TankPawn>();
        if (tankPawn != null)
        {
            //The second parameter is the pawn who caused the increased speed- in this case they increased their own speed.
            tankPawn.IncreaseSpeed(speedToAdd, target.GetComponent<Pawn>());
        }
    }

    public override void Remove(PowerupManager target)
    {
        //TODO:  Remove item
        TankPawn tankPawn = target.GetComponent<TankPawn>();
        if (tankPawn == null)
        {
            tankPawn.DecreaseSpeed(speedToRemove, target.GetComponent<Pawn>());
        }
    }
}
