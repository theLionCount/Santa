using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerWeapon.dmg *= 1.17f;
        playerWeapon.bulletSizeMod *= 1.17f;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
+17% damage dealt";

    }

    public override string getDoorPrompt()
    {
        return "Character buff";
    }
}
