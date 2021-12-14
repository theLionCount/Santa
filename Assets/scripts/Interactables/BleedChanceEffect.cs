using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedChanceEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerWeapon.bleedChance = playerWeapon.bleedChance <= 0 ? 0.15f : (playerWeapon.bleedChance * 1.15f);
    }
}
