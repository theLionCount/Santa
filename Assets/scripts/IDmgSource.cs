using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDmgSource
{
    public float hitTarget(IDmgTarget target);

    public float getDamage();

    public bool crit();

    public float bleedChance();

    public float healthyDmg();
}
