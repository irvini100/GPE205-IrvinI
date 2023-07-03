using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class SpeedPowerup : Powerup
{
    public float speedToAdd;
    public override void Apply(PowerupManager target)
    {
        target.GetComponent<Pawn>().moveSpeed += speedToAdd;
    }

    public override void Remove(PowerupManager target)
    {
        //TODO:  Remove item
    }
}
