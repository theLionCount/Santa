using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMovementBAse : GroundMovementBase
{
    public float rollSpeedModifier = 1.4f;
    public int rollNum = 1;
    public float rollCDTime;

    float rollCD;
    int currentRolls;

    public bool reloadonRoll;

    public WeaponController weaponController;

    public bool midRoll;

    int startedRolls;
    Vector2 rDir;

    protected Animator anim;


    protected override void Start()
    {
        base.Start();
        weaponController = gameObject.GetComponentInChildren<WeaponController>();
        anim = GetComponent<Animator>();
    }

    public override void move(Vector2 dir)
    {
        if (!midRoll) movementDir = dir;
    }

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

    public override void FixedUpdate()
    {
        if (!midRoll && startedRolls > 0)
        {
            roll(rDir);
            startedRolls--;

        }
        rollCD--;
        if (rollCD <= 0) currentRolls = rollNum;
        collidedMove(movementDir.normalized * speed * (midRoll ? rollSpeedModifier : 1));
        anim.SetBool("Run", movementDir != Vector2.zero);
    }
}
