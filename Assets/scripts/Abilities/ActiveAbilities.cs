using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbilities : MonoBehaviour
{
    public float bulletTimeSpeed;
    public float bulletTimeDuration;

    public float rampageDuration;
    int normalMagsize;
    float normalRecovery;

    public float clearDuration;

    public int charges;
    public int maxCharges;

    public string selectedAbility;

    float cd;
    bool active;

    bool spacePressed;


    protected GameObject playerObject;
    protected Character playerChar;
    protected MagazinedWeapon playerWeapon;
    protected CharacterWeaponController weaponController;
    protected VisualReload reload;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        playerChar = playerObject.GetComponent<Character>();
        playerWeapon = playerObject.GetComponentInChildren<MagazinedWeapon>();
        weaponController = playerObject.GetComponentInChildren<CharacterWeaponController>();
        reload = playerWeapon.GetComponent<VisualReload>();
    }

    public void resetCharges()
    {
        charges = maxCharges;
    }

    private void FixedUpdate()
    {
        if (spacePressed && !active && charges > 0)
        {
            charges--;
            if (selectedAbility == "BulletTime") bulletTime();
            if (selectedAbility == "Rampage") rampage();
            if (selectedAbility == "Clear") clear();
        }
        if (spacePressed) spacePressed = false;
        if (active)
        {
            cd--;
            if (cd <= 0)
            {
                active = false;
                if (selectedAbility == "BulletTime") endBulletTime();
                if (selectedAbility == "Rampage") endRampage();
            }
            else
            {
                if (selectedAbility == "Clear") clearFrame();
            }
        }
    }

    void bulletTime()
    {
        cd = bulletTimeDuration;
        active = true;
        Time.timeScale = bulletTimeSpeed;
    }

    void endBulletTime()
    {
        Time.timeScale = 1;
    }


    void rampage()
    {
        playerWeapon = playerObject.GetComponentInChildren<MagazinedWeapon>();
        weaponController = playerObject.GetComponentInChildren<CharacterWeaponController>();
        cd = rampageDuration;
        active = true;
        normalMagsize = playerWeapon.magSize;
        normalRecovery = playerWeapon.coolDown;
        playerWeapon.magSize = 9999;
        playerWeapon.bulletsInMag = 9999;
        playerWeapon.coolDown = Mathf.Sqrt(playerWeapon.coolDown);
    }

    void endRampage()
    {
        playerWeapon = playerObject.GetComponentInChildren<MagazinedWeapon>();
        weaponController = playerObject.GetComponentInChildren<CharacterWeaponController>();
        playerWeapon.magSize = normalMagsize;
        playerWeapon.bulletsInMag = normalMagsize;
        playerWeapon.coolDown = normalRecovery;
    }

    void clear()
    {
        active = true;
        cd = clearDuration;
    }

    void clearFrame()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("EnemyProjectile"))
        {
            Destroy(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) spacePressed = true;
    }
}
