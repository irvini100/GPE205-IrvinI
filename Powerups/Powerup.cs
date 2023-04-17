using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    public float duration;
    public bool isPermanent;
    public abstract void Apply(PowerupManager target);
    public abstract void Remove(PowerupManager target);
}
