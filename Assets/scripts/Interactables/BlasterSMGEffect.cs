using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterSMGEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        // playerChar.health = playerChar.maxHealth;
        //playerWeapon.magSize *= 2;
        playerWeapon.coolDown = 0.3f;
        playerWeapon.spreadAngle = 11;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
SMG trigger mod
Greatly increases fire rate and mag size at the cost of accuracy";

    }

    public override string getDoorPrompt()
    {
        return "Weapon part";
    }
}
