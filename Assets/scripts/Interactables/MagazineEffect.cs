using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        // playerChar.health = playerChar.maxHealth;
        playerWeapon.magSize = (int)(playerWeapon.magSize * 1.5f);
        //playerWeapon.coolDown *= 0.75f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
+1.5x magazine size";

    }

    public override string getDoorPrompt()
    {
        return "Weapon part";
    }
}
