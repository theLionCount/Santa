using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirerateEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
       // playerChar.health = playerChar.maxHealth;
        //playerWeapon.magSize *= 2;
        playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
+20% fire rate";

    }

    public override string getDoorPrompt()
    {
        return "Weapon part";
    }
}
