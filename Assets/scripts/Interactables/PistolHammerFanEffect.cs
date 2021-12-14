using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolHammerFanEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerWeapon.coolDown = 2.5f;
        weaponController.burstFire = false;
        weaponController.semiAuto = false;
        playerWeapon.spreadAngle = 22f;
    }
}

