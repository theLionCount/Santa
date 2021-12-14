using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolReloadOnRollEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerChar.reloadonRoll = true;
    }
}
