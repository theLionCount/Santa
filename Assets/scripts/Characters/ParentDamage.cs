using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDamage : MonoBehaviour
{
    IDmgTarget target;
    DamageNumberCreator damageNumbers;

    bool resist;
    public void setResistance(bool r)
    {
        resist = r;
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponentInParent<IDmgTarget>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!resist)
        {
            var source = collision.collider.GetComponent<IDmgSource>();
            if (source != null)
            {
                target.damageHit(source);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
