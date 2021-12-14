using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBase : MonoBehaviour, IMovable
{
    public float speed;

    Animator anim;
    Rigidbody2D rb2d;
    ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    Aiming gunAim;

    public WeaponController weaponController;

    public GameObject explosion;
    public HealthTracker health;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask.GetMask("Walls"));
        contactFilter.useLayerMask = true;
        gunAim = gameObject.GetComponentInChildren<Aiming>();
        weaponController = gameObject.GetComponentInChildren<WeaponController>();
        health = GetComponent<HealthTracker>();
    }

    public void move(Vector2 dir)
    {
        movementDir = dir;
    }

    Vector2 movementDir;

    bool died;

    public virtual void FixedUpdate()
    {

        if (health.health <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(gameObject);
        }

        collidedMove(movementDir.normalized * speed );
        transform.localScale = movementDir != Vector2.zero ? new Vector3(movementDir.x > 0 ? 1 : -1, 1, 1) : new Vector3(gunAim.dir.x > 0 ? 1 : -1, 1, 1);
    }

    public void collidedMove(Vector2 to)
    {
        Vector2 horizontal = new Vector2(to.x, 0);
        Vector2 vertical = new Vector2(0, to.y);

        float magnitudeAdjustment = rb2d.Cast(horizontal, contactFilter, hitBuffer, 0) > 0 ? 3f : 2f;


        if (rb2d.Cast(horizontal, contactFilter, hitBuffer, horizontal.magnitude * magnitudeAdjustment) <= 0) rb2d.position += horizontal;
        if (rb2d.Cast(vertical, contactFilter, hitBuffer, vertical.magnitude * magnitudeAdjustment) <= 0) rb2d.position += vertical;
    }
}
