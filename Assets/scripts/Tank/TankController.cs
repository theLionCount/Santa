using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    GameObject player;
    AStarMovement movement;
    TankBase body;
    Animator anim;
    ParticleSystem smoketrail;
    EnemyWeaponController weaponController;
    TankAim aim;
    ParentDamage damager;

    bool isShooting;
    bool moving;
    bool tforming;
    bool cdToOpen;

    public float afterReloadCooldown;
    public float afterStoppedCooldown;
    public float afterShootCooldown;
    float cd;

    Vector3 movementTarget;

    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        movement = GetComponent<AStarMovement>();
        body = GetComponent<TankBase>();
        anim = GetComponent<Animator>();
        smoketrail = GetComponentInChildren<ParticleSystem>();
        aim = GetComponentInChildren<TankAim>();
        layerMask = LayerMask.GetMask("Walls");
        weaponController = GetComponentInChildren<EnemyWeaponController>();
        damager = GetComponentInChildren<ParentDamage>();

        weaponController.reload();
        isShooting = false;
        tforming = true;
        setMovementTarget(8, 1000, 10000000);
        movement.targetPos = movementTarget;
        cd = afterShootCooldown;
        cdToOpen = false;
        finishedClosing();
    }

    

    public void finishedOpening()
    {
        cd = afterStoppedCooldown;
        isShooting = true;
        tforming = false;
        weaponController.gameObject.SetActive(true);
        damager.setResistance(false);
    }

    public void finishedClosing()
    {
        smoketrail.Play();
        moving = true;
        tforming = false;
    }
    void open()
    {
        anim.SetBool("Open", true);
        anim.SetBool("Close", false);
        smoketrail.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        tforming = true;
        aim.setInitialTarget();
    }

    void close()
    {
        weaponController.gameObject.SetActive(false);
        anim.SetBool("Open", false);
        anim.SetBool("Close", true);
        tforming = true;
        damager.setResistance(true);
    }

    private void FixedUpdate()
    {
        cd--;
        if (isShooting && cd <= 0)
        {
            movement.stop();
            aim.setFireAim(1 - (2 * (float)weaponController.mw.bulletsInMag / weaponController.mw.magSize));
            weaponController.fire();
            if (weaponController.needsReload())
            {
                weaponController.reload();
                isShooting = false;
                tforming = true;
                setMovementTarget(10, 1000, 10000000);
                movement.targetPos = movementTarget;
                cd = afterShootCooldown;
                cdToOpen = false;
            }
        }
        else
        {
            if (tforming)
            {
                movement.stop();
                if (cd <= 0)
                {
                    if (cdToOpen) open();
                    else close();
                }
            }

            if (moving)
            {
                movement.move();
                if (movement.atTarget())
                {
                    moving = false;
                    tforming = true;
                    cdToOpen = true;
                    cd = afterStoppedCooldown;
                }
                //else if (!reaction && cd <= 0 && Physics2D.Linecast(transform.position, player.transform.position, layerMask).collider == null)
                //{
                //    setMovementTarget(1, 5, 1000);
                //    movement.targetPos = movementTarget;
                //    reaction = true;
                //}
            }
            else
            {
                movement.stop();
            }
        }
    }

    void setMovementTarget(int minRadius, int maxRadius, int maxRuns)
    {
        int r = minRadius;
        int runs = 0;
        do
        {
            do
            {
                runs++;
                if (runs > maxRuns) break;
                if (r < maxRadius) r += 2;
                movementTarget = new Vector3(Random.Range(-r, r), Random.Range(-r, r), 0) + transform.position;
            } while (!movement.isPointOk(movementTarget));
            if (runs > maxRuns) break;
        } while (Physics2D.Linecast(movementTarget, player.transform.position, layerMask).collider != null);
        if (runs > maxRuns) movementTarget = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
