using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkinMovement : GroundMovementBase
{
    protected Animator anim;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        anim.SetBool("Run", movementDir != Vector2.zero);
    }
}
