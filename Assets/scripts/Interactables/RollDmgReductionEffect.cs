using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDmgReductionEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        health.rollDamageReduction *= 0.6f;
        //playerWeapon.magSize *= 2;
        //playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
+60% armour while rolling";

    }

    public override string getDoorPrompt()
    {
        return "Roll damage reduction";
    }
}
