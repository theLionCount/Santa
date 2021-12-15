using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingCharacter : MonoBehaviour
{
    protected Aiming gunAim;
    protected Animator anim;
    protected IHaveMovementdir movementdir;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        GetComponent<IHaveMovementdir>();
        gunAim = gameObject.GetComponentInChildren<Aiming>();
        anim = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        transform.localScale = new Vector3(gunAim.dir.x > 0 ? 1 : -1, 1, 1);
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerRun")) && movementdir.getMovementDir().x * gunAim.dir.x < 0) anim.SetFloat("Dir", -1);
        else anim.SetFloat("Dir", 1);
    }
}
