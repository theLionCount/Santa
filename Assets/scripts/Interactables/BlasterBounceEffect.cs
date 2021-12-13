using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBounceEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        // playerChar.health = playerChar.maxHealth;
        //playerWeapon.magSize *= 2;
        playerWeapon.secondaryTarget = true;
        
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
Ricochet ammo mod
On hit, the bullet ricochets to a nerby enemy";

    }

    public override string getDoorPrompt()
    {
        return "Weapon part";
    }
}