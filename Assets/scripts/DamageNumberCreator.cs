using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberCreator : MonoBehaviour
{
    public GameObject damageNumberBase;
    float currentDamage;

    DamageNumber current;

    public float newTickWaitTime;
    float cd;

    public void message(string msg)
    {
        var nDmgn = Instantiate(damageNumberBase, transform).GetComponent<DamageNumber>();
        nDmgn.transform.localPosition = nDmgn.transform.localPosition + new Vector3(1.5f, 0, 0);
        nDmgn.setMessage(msg);
        nDmgn.fadeOut();
    }

    public void damage(float damage, bool crit, bool alternativeSource)
    {
        if (!alternativeSource)
        {
            if (crit) releas();
            currentDamage += damage;
            if (current == null) current = Instantiate(damageNumberBase, transform).GetComponent<DamageNumber>();
            current.setDamage(currentDamage, crit);
            cd = newTickWaitTime;
        }
        else
        { 
            var nDmgn = Instantiate(damageNumberBase, transform).GetComponent<DamageNumber>();
            nDmgn.transform.localPosition = nDmgn.transform.localPosition + new Vector3(1.5f, 0, 0);
            nDmgn.setDamage(damage, crit);
            nDmgn.fadeOut();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        current = null;
    }

    void releas()
    {
        if (current != null)
        {
            current.fadeOut();
            currentDamage = 0;
            current = null;
        }
    }

    private void FixedUpdate()
    {
        cd--;
        if (cd <= 0 && current != null)
        {
            releas();
        }
    }

    void Update()
    {
        transform.localScale = new Vector3(transform.parent.localScale.x, 1, 1);
    }
}
