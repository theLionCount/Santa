using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterMagSizeEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        // playerChar.health = playerChar.maxHealth;
        playerWeapon.magSize = (int)(playerWeapon.magSize * 2);
        //playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
Drum mag magazine update
Greatly increases mag size";

    }

    public override string getDoorPrompt()
    {
        return "Magazine mod";
    }
}