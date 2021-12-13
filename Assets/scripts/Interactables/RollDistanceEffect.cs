using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDistanceEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerChar.rollSpeedModifier *= 1.3f;
        //playerWeapon.magSize *= 2;
        //playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
+30 roll distance";

    }

    public override string getDoorPrompt()
    {
        return "Roll buff";
    }
}
