using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBurstEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        // playerChar.health = playerChar.maxHealth;
        //playerWeapon.magSize *= 2;
        playerWeapon.coolDown = 0.3f;
        weaponController.burstFire = true;
        weaponController.burstSize = 5;
        playerWeapon.spreadAngle = 0.8f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
Burst fire trigger mod
Changes the fire mode to accurate, high damage 5 bullet burst";

    }

    public override string getDoorPrompt()
    {
        return "Weapon part";
    }
}
