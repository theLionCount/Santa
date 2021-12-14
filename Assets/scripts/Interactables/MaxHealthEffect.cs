using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthEffect : InteractEffect
{

    public float plusHealth;
    public override void doEffect()
    {
        base.doEffect();
        health.maxHealth += plusHealth;
        health.health += plusHealth;
        //playerWeapon.magSize *= 2;
        //playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
+25 max health";

    }

    public override string getDoorPrompt()
    {
        return "Max health";
    }
}

