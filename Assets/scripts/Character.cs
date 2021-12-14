using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IMovable
{
    public float speed;

    Animator anim;
    Rigidbody2D rb2d;
    ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    Aiming gunAim;

    public WeaponController weaponController;

    public float rollSpeedModifier = 1.4f;
    public int rollNum = 1;
    public float rollCDTime;

    float rollCD;
    int currentRolls;

    public bool reloadonRoll;

    HealthTracker health;

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
        if (!midRoll) movementDir = dir;        
    }

    public bool midRoll;

    int startedRolls;
    Vector2 rDir;
    public void startRoll(Vector2 dir)
    {
        rDir = dir;
        startedRolls++;
        if (startedRolls > currentRolls) startedRolls = currentRolls;
    }

    public void roll(Vector2 dir)
    {
        if (!midRoll && currentRolls > 0)
        {
            if (reloadonRoll) weaponController.blaster.reload(true);
            movementDir = dir;
            midRoll = true;
            anim.SetTrigger("Roll");
            weaponController.blaster.gameObject.SetActive(false);
            rollCD = rollCDTime;
            currentRolls--;
        }
    }

    public void endRoll()
    {
        midRoll = false;
        weaponController.blaster.gameObject.SetActive(true);
    }

    Vector2 movementDir;

    public virtual void FixedUpdate()
    {
        if (!midRoll && startedRolls > 0)
        {
            roll(rDir);
            startedRolls--;

        }
        rollCD--;
        if (rollCD <= 0) currentRolls = rollNum;
        if (health.health <= 0)
        {
            anim.SetTrigger("Die");
            if (weaponController != null && weaponController.gameObject != null) Destroy(weaponController.gameObject);
        }

        collidedMove(movementDir.normalized * speed * (midRoll ? rollSpeedModifier : 1));
        anim.SetBool("Run", movementDir != Vector2.zero);
        transform.localScale = midRoll ? new Vector3(movementDir.x > 0 ? 1 : -1, 1, 1) : new Vector3(gunAim.dir.x > 0 ? 1 : -1, 1, 1);
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerRun")) && movementDir.x * gunAim.dir.x < 0) anim.SetFloat("Dir", -1);
        else anim.SetFloat("Dir", 1);

    }

    public void collidedMove(Vector2 to)
    {
        Vector2 horizontal = new Vector2(to.x, 0);
        Vector2 vertical = new Vector2(0, to.y);

        float magnitudeAdjustment = rb2d.Cast(horizontal, contactFilter, hitBuffer, 0) > 0 ? 3f : 2f;

        

        if (rb2d.Cast(horizontal, contactFilter, hitBuffer, horizontal.magnitude * magnitudeAdjustment) <= 0) rb2d.position += horizontal;
        if (rb2d.Cast(vertical, contactFilter, hitBuffer, vertical.magnitude * magnitudeAdjustment) <= 0) rb2d.position += vertical;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
