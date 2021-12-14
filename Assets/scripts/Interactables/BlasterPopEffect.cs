using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterPopEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        var blt = playerWeapon.pop = 3;
        
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
Scatter ammo mod
On impact, the bullets scatters into multiple projectiles";

    }

    public override string getDoorPrompt()
    {
        return "Ammo mod";
    }
}
