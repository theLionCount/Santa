using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillHealthEffect : InteractEffect
{
    public float healthPercentage;

    public override void doEffect()
    {
        //base.doEffect();
        health.health += health.maxHealth * healthPercentage;
        if (health.health > health.maxHealth) health.health = health.maxHealth;
        //playerWeapon.magSize *= 2;
        //playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
+" + ((int)(healthPercentage * 100)).ToString() + "% health";

    }

    public override string getDoorPrompt()
    {
        return "Healing";
    }
}
