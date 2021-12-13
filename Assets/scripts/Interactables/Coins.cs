using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : InteractEffect
{
    public override void doEffect()
    {
        //base.doEffect();
        //playerChar.health += playerChar.maxHealth * 0.4f;
        //if (playerChar.health > playerChar.maxHealth) playerChar.health = playerChar.maxHealth;
        //playerWeapon.magSize *= 2;
        //playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
+25 coins";

    }

    public override string getDoorPrompt()
    {
        return "Coins";
    }
}
