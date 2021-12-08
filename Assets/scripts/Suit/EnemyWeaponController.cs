using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : WeaponController
{
    public float cooldown;
    float cd;
    MagazinedWeapon mw;

    private void Start()
    {
        cd = cooldown * 2 + Random.Range(0, cooldown);
        mw = blaster as MagazinedWeapon;
    }

    private void FixedUpdate()
    {
        if (mw != null && mw.bulletsInMag <= 0) cd--;
        else cd = cooldown;

        //cd--;
        //if (cd <= 0)
        //{
        //    if (mw != null && mw.bulletsInMag <= 0) mw.reload();
        //    else blaster.fire();
        //    cd = cooldown;  
        //}
    }

    public void fire()
    {
        blaster.fire();
    }

    public void reload()
    {
        if (mw != null) mw.reload();
    }

    public bool needsReload()
    {
        return cd <= 0 && mw != null && mw.bulletsInMag <= 0;
    }

    public bool reloadFinished()
    {
        return mw != null && mw.bulletsInMag > 0;
    }
}
