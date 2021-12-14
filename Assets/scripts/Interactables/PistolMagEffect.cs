using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolMagEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerWeapon.magSize *= 2;
    }
}

