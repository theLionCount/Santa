using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterRldSpeedEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        // playerChar.health = playerChar.maxHealth;
        reload.rldSpeed *= 2;
        //playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
Speed swapper magazine update
Greatly reduces time to reload";

    }

    public override string getDoorPrompt()
    {
        return "Magazine mod";
    }
}