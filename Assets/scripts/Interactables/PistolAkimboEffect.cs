using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAkimboEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerWeapon.coolDown = 3.5f;
        playerWeapon.magSize *= 2;
        weaponController.burstFire = true;
        weaponController.semiAuto = false;
        weaponController.burstSize = 2;
    }
}
