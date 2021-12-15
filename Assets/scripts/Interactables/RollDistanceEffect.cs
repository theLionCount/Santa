using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDistanceEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerRollMovement.rollSpeedModifier *= 1.13f;
        //playerWeapon.magSize *= 2;
        //playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
Roll farther";

    }

    public override string getDoorPrompt()
    {
        return "Roll distance";
    }
}
