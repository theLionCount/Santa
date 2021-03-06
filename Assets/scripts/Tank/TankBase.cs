using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBase : MonoBehaviour
{
    Animator anim;
    Aiming gunAim;

    public WeaponController weaponController;

    public GameObject explosion;
    public HealthTracker health;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        gunAim = gameObject.GetComponentInChildren<Aiming>();
        weaponController = gameObject.GetComponentInChildren<WeaponController>();
        health = GetComponent<HealthTracker>();
    }

    bool died;

    public virtual void FixedUpdate()
    {

        if (health.health <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(gameObject);
        }
    }
}
