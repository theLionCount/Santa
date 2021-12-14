using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : InteractEffect
{
    public float maxBulletSize = 1.8f;

    public override void doEffect()
    {
        base.doEffect();
        playerWeapon.dmg *= 1.1f;
        playerWeapon.bulletSizeMod *= 1.07f;
        if (playerWeapon.bulletSizeMod > maxBulletSize) playerWeapon.bulletSizeMod = maxBulletSize;
    }

    public override string getPrompt()
    {
        return @"Press F to pick up!
+10% damage dealt";

    }

    public override string getDoorPrompt()
    {
        return "Bonus damage";
    }
}
