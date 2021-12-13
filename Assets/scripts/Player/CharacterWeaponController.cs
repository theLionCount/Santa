using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponController : WeaponController
{

    bool fireOn;
    bool emptyMag;
    bool startedReload;
    Reloader reloader;
    MagazinedWeapon mw;
    public bool semiAuto;
    public bool burstFire;
    public int burstSize;

    // Start is called before the first frame update
    void Start()
    {
        blaster = GetComponent<BaseWeapon>();
        reloader = GetComponent<Reloader>();
        mw = GetComponent<MagazinedWeapon>();
        fireOn = true;
    }

    bool mbd;

    private void Update()
    {
        mbd = Input.GetMouseButton(0);
        if (!mbd)
        {
            fireOn = true;
        }
        if (Input.GetKeyDown(KeyCode.R)) blaster.reload();
    }

    private void FixedUpdate()
    {
        if (reloader != null)
        {
            if (reloader.reloadInProgress)
            {
               // if (!startedReload) fireOn = false;
                startedReload = true;
            }
            else
            {
                startedReload = false;
            }
        }

        if (mw != null)
        {
            if (mw.bulletsInMag <= 0)
            {
                if (!emptyMag) fireOn = false;
                emptyMag = true;
            }
            else
            {
                emptyMag = false;
            }
        }

        if (burstFire && blaster.currntBustSize >= burstSize) fireOn = false;

        if (mbd && fireOn)
        {
            if (!emptyMag)
            {
                blaster.fire();
                if (semiAuto) fireOn = false;
            }
            else
            {
                mw.reload();
            }
        }


    }
}