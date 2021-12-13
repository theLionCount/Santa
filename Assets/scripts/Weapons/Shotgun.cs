using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MagazinedWeapon
{
    public int bullets;
    public float bulletAngle;

    protected override void shootBullet()
    {
        currntBustSize++;
        var spreadStart = Random.Range(-spreadAngle, spreadAngle);

        for (int i = -bullets; i <= bullets; i++)
        {
            var spread = spreadStart + i * bulletAngle;
            var blt = Instantiate(bullet, barrel.transform.position, Quaternion.Euler(0, 0, aim.angle + spread)).GetComponent<Bullet>();
            blt.dir = Quaternion.Euler(0, 0, spread) * aim.dir.normalized;
            blt.dmg = dmg;
            blt.pop = pop;
            blt.secondaryTarget = secondaryTarget;
            blt.transform.localScale = blt.transform.localScale * bulletSizeMod;
        }

        //TODO screenshake here
    }
}
