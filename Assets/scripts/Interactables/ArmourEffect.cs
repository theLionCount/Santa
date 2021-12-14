using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        health.dmgRed *= 0.9f;
        //playerWeapon.magSize *= 2;
        //playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
+10% armour";

    }

    public override string getDoorPrompt()
    {
        return "Armour";
    }
}
