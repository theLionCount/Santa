using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    Character player;
    BaseWeapon weapon;
    public bool buff;
    public float buffDuration;
    bool currentlyBuffed;
    float cd;
    float normalDmg;
    float normalArmour;
    float normalSpeed;

    public float dmgMod = 1.2f;
    public float armourMod = 0.7f;
    public float speedMod = 1.2f;
    HealthTracker health;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Character>();
        health = GetComponent<HealthTracker>();
        weapon = GetComponentInChildren<BaseWeapon>();
    }

    public void EnemyDead()
    {
        if (buff)
        {
            if (!currentlyBuffed)
            {
                currentlyBuffed = true;
                normalDmg = weapon.dmg;
                normalArmour = health.dmgRed;
                normalSpeed = player.speed;
            }
            weapon.dmg = normalDmg * dmgMod;
            health.dmgRed = normalArmour * armourMod;
            player.speed = normalSpeed * speedMod;
            cd = buffDuration;
        }
    }

    private void FixedUpdate()
    {
        cd--;
        if (currentlyBuffed && cd <= 0)
        {
            currentlyBuffed = false;
            weapon.dmg = normalDmg;
            health.dmgRed = normalArmour;
            player.speed = normalSpeed;
        }
    }
}
