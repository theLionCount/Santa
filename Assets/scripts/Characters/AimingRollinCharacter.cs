using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingRollinCharacter : AimingCharacter
{
    // Start is called before the first frame update
    RollMovementBAse rollMove;
    protected override void Start()
    {
        base.Start();
        rollMove = GetComponent<RollMovementBAse>();
    }

    public Vector2 aim;

    protected override void FixedUpdate()
    {
        if (gunAim == null || !gunAim.gameObject.activeInHierarchy)
            gunAim = gameObject.GetComponentInChildren<Aiming>();
        if (gunAim != null) aim = gunAim.dir;
        transform.localScale = rollMove.midRoll ? new Vector3(rollMove.getMovementDir().x > 0 ? 1 : -1, 1, 1) : new Vector3(aim.x > 0 ? 1 : -1, 1, 1);
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerRun")) && rollMove.getMovementDir().x * aim.x < 0) anim.SetFloat("Dir", -1);
        else anim.SetFloat("Dir", 1);
    }
}
