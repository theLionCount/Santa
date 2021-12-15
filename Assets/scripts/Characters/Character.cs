using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Animator anim;
    public WeaponController weaponController;

    HealthTracker health;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        weaponController = gameObject.GetComponentInChildren<WeaponController>();
        health = GetComponent<HealthTracker>();
    }



    public virtual void FixedUpdate()
    {

        if (health.health <= 0)
        {
            anim.SetTrigger("Die");
            if (weaponController != null && weaponController.gameObject != null) Destroy(weaponController.gameObject);
        }
    }
}
