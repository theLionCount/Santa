using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazinedWeapon : BaseWeapon
{

    public int magSize;
    public int bulletsInMag;

    Reloader reloader;
    Reloader fastReloader;

    public override void Start()
    {
        base.Start();
        reloader = GetComponents<Reloader>()[0];
        if (GetComponents<Reloader>().Length>1) fastReloader = GetComponents<Reloader>()[1];
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

    public override void reload(bool fast = false)
    {
        if (magSize > bulletsInMag)
        {
            if (!fast) reloader.startReload();
            else fastReloader.startReload();
        }
    }

    public void finishReload()
    {
        bulletsInMag = magSize;
    }
    
}
