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

    public void damage(float damage)
    {
        currentDamage += damage;
        if (current == null) current = Instantiate(damageNumberBase, transform).GetComponent<DamageNumber>();
        current.setDamage(currentDamage);
        cd = newTickWaitTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        current = null;
    }

    private void FixedUpdate()
    {
        cd--;
        if (cd <= 0 && current != null)
        {
            current.fadeOut();
            currentDamage = 0;
            current = null;
        }
    }

    void Update()
    {
        transform.localScale = new Vector3(transform.parent.localScale.x, 1, 1);
    }
}
