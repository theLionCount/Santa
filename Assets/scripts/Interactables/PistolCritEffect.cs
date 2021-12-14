using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolCritEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerWeapon.critChance = 0.12f;
    }
}
