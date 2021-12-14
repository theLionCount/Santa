using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollNumberEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerChar.rollNum+=2;
        //playerWeapon.magSize *= 2;
        //playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
+2 roll";

    }

    public override string getDoorPrompt()
    {
        return "Roll number buff";
    }
}