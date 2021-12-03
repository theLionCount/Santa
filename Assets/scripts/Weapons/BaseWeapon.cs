using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject muzzleFlash;
    public GameObject barrel;
    protected WeaponController weaponController;
    protected Aiming aim;

    public float coolDown;
    protected float cd;

    public int currntBustSize;

    public float spreadAngle;

    // Start is called before the first frame update
    public virtual void Start()
    {
        aim = GetComponent<Aiming>();
        weaponController = GetComponent<WeaponController>();
    }

    private void FixedUpdate()
    {
        cd--;
        if (cd < -1) currntBustSize = 0;
    }

    public virtual void fire()
    {
        if (cd <= 0)
        {
            cd = coolDown;
            doMuzzleFlash();
            shootBullet();
        }
    }

    protected virtual void shootBullet()
    {
        currntBustSize++;
        var spread = Random.Range(-spreadAngle, spreadAngle);

        var blt = Instantiate(bullet, barrel.transform.position, Quaternion.Euler(0, 0, aim.angle + spread)).GetComponent<Bullet>();
        blt.dir = Quaternion.Euler(0, 0, spread) * aim.dir.normalized;
    }

    protected virtual void doMuzzleFlash()
    {
        cd = coolDown;
        var flash = Instantiate(muzzleFlash, barrel.transform.position, Quaternion.identity, barrel.transform);
        flash.transform.parent = barrel.transform;
    }

    public virtual void reload()
    { 
    }
}
