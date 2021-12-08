using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitController : MonoBehaviour
{
    GameObject player;
    AStarMovement movement;
    EnemyWeaponController weaponController;
    Vector3 movementTarget;

    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        movement = GetComponent<AStarMovement>();
        weaponController = GetComponentInChildren<EnemyWeaponController>();
        isShooting = true;

        layerMask = LayerMask.GetMask("Walls");
        cd = afterReloadCooldown * (1f + Random.value);
    }

    bool isShooting;
    bool reloading;
    bool moving;
    bool reaction;

    public float afterReloadCooldown;
    public float afterStoppedCooldown;
    public float afterShootCooldown;
    float cd;

    private void FixedUpdate()
    {
        cd--;
        if (isShooting && cd <= 0)
        {
            movement.stop();
            weaponController.fire();
            if (weaponController.needsReload())
            {
                weaponController.reload();
                isShooting = false;
                reloading = true;
                moving = true;

                setMovementTarget(5, 1000, 10000000);
                movement.targetPos = movementTarget;
            }
        }
        else
        { 
            if (reloading && weaponController.reloadFinished())
            { 
                reloading = false;
                cd = afterReloadCooldown;
            }
            if (moving)
            {
                movement.move();
                if (movement.atTarget())
                {
                    reaction = false;
                    moving = false;
                    cd = afterStoppedCooldown;
                }
                else if (!reaction && cd <= 0 && Physics2D.Linecast(transform.position, player.transform.position, layerMask).collider == null)
                {
                    setMovementTarget(1, 5, 1000);
                    movement.targetPos = movementTarget;
                    reaction = true;
                }
            }
            else
            {
                movement.stop();
            }
            if (cd <= 0 && !reloading && !moving) isShooting = true;
        }
    }

    List<RaycastHit2D> rch2d = new List<RaycastHit2D>();

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
