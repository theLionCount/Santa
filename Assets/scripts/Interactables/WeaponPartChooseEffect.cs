using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPartChooseEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
    }

    public override string getPrompt()
    {
        return @"U shouldnt see this";

    }

    public override string getDoorPrompt()
    {
        return "Weapon part";
    }
}
