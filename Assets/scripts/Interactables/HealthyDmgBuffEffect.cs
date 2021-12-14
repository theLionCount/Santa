using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyDmgBuffEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerWeapon.healthyDmgModifier *= 1.5f;
    }
}
