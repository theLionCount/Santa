using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSpeedEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerRollMovement.speed *= 1.14f;
    }
}