using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;

    Animator anim;
    Rigidbody2D rb2d;
    ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    Aiming gunAim;
    float lastAnimSpeed;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask((Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("Walls"))));
        contactFilter.useLayerMask = true;
        gunAim = gameObject.GetComponentInChildren<Aiming>();
    }

    public void move(Vector2 dir)
    {
        collidedMove(dir.normalized * speed);
        anim.SetBool("Run", dir != Vector2.zero);
        transform.localScale = new Vector3(gunAim.dir.x > 0 ? 1 : -1, 1, 1);
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerRun")) && dir.x * gunAim.dir.x < 0) anim.speed = -1;
        else anim.speed = 1;
        if (lastAnimSpeed != anim.speed)
        {
            anim.StartPlayback();
            lastAnimSpeed = anim.speed;
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
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
