using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollNumberEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerChar.rollNum++;
        //playerWeapon.magSize *= 2;
        //playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
+1 roll";

    }

    public override string getDoorPrompt()
    {
        return "Roll buff";
    }
}