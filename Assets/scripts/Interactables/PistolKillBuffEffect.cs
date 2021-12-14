using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolKillBuffEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        playerObject.GetComponent<KillCounter>().buff = true;
    }
}