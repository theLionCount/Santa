using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : MonoBehaviour, IDmgTarget
{

    public float health;
    public float maxHealth;
    KillCounter killCounter;
    bool died;
    public float dmgRed;
    public float rollDamageReduction;
    DamageNumberCreator damageNumbers;
    Animator anim;
    WeaponController weaponController;

    Character body;

    public bool player;


    public float bleedDmg;
    public float bleedDuration;
    public float bleedTickIntervall;
    List<float> bleedCDs = new List<float>();
    float cd;

    public float hitchance = 1;

    // Start is called before the first frame update
    void Start()
    {
        killCounter = GameObject.Find("Player").GetComponent<KillCounter>();
        anim = GetComponent<Animator>();
        damageNumbers = GetComponentInChildren<DamageNumberCreator>();
        weaponController = gameObject.GetComponentInChildren<WeaponController>();
        body = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!died)
        {
            died = true;
            killCounter.EnemyDead();
        }
        cd++;
        if (cd > bleedTickIntervall && bleedCDs.Count>0)
        {
            cd = 0;
            alternativeDmg(bleedCDs.Count * bleedDmg);
        }
        for (int i = 0; i < bleedCDs.Count; i++)
        {
            bleedCDs[i]--;
        }
        bleedCDs.RemoveAll(t => t < 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var source = collision.collider.GetComponent<IDmgSource>();
        if (source != null) damageHit(source);
    }

    public void alternativeDmg(float dmg)
    {
        
        var realDmg = dmg * (!(body != null && body.midRoll) ? 1 : rollDamageReduction) * dmgRed;
        health -= realDmg;
        if (health + realDmg > 0) damageNumbers.damage(realDmg, false, true);
    }

    public void damageHit(IDmgSource source)
    {
        if (Random.Range(0f, 1f) < hitchance)
        {
            if (source.bleedChance() > 0 && Random.Range(0f, 1f) < source.bleedChance())
            {
                bleedCDs.Add(bleedDuration);
            }

            var dmg = source.hitTarget(this);
            if (source.healthyDmg() > 1 && health >= maxHealth * 0.8f) dmg *= source.healthyDmg();
            var realDmg = dmg * (!(body != null && body.midRoll) ? 1 : rollDamageReduction) * dmgRed;
            health -= realDmg;
            if (health + realDmg > 0) damageNumbers.damage(realDmg, source.crit(), false);

            anim.SetTrigger("Hit");
            if (weaponController != null)
            {
                weaponController.blaster.onHit();
            }
        }
        else
        {
            damageNumbers.message("MISSED");
        }
    }
}
