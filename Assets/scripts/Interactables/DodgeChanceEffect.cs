using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeChanceEffect : InteractEffect
{
    public override void doEffect()
    {
        base.doEffect();
        health.hitchance = health.hitchance == 1 ? 0.8f : (health.hitchance * 0.9f);
    }
}