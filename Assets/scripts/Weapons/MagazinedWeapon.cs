using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazinedWeapon : BaseWeapon
{

    public int magSize;
    public int bulletsInMag;

    Reloader reloader;

    public override void Start()
    {
        base.Start();
        reloader = GetComponent<Reloader>();
        bulletsInMag = magSize;
    }

    public override void fire()
    {
        if (cd <= 0)
        {
            if (bulletsInMag > 0)
            {
                cd = coolDown;
                doMuzzleFlash();
                shootBullet();
                bulletsInMag--;
                if (reloader.reloadInProgress) reloader.cancelReload();
            }
        }
    }

    public override void reload()
    {
        if (magSize>bulletsInMag) reloader.startReload();
    }

    public void finishReload()
    {
        bulletsInMag = magSize;
    }
    
}
