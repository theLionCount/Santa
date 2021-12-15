using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovementBase : MonoBehaviour, IMovable, IHaveMovementdir
{
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    public float speed;

    public virtual void move(Vector2 dir)
    {
        movementDir = dir;
    }

    protected Vector2 movementDir;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask.GetMask("Walls"));
        contactFilter.useLayerMask = true;
    }

    public virtual void FixedUpdate()
    {
        collidedMove(movementDir.normalized * speed);
    }

    public void collidedMove(Vector2 to)
    {
        Vector2 horizontal = new Vector2(to.x, 0);
        Vector2 vertical = new Vector2(0, to.y);

        float magnitudeAdjustment = rb2d.Cast(horizontal, contactFilter, hitBuffer, 0) > 0 ? 3f : 2f;


        if (rb2d.Cast(horizontal, contactFilter, hitBuffer, horizontal.magnitude * magnitudeAdjustment) <= 0) rb2d.position += horizontal;
        if (rb2d.Cast(vertical, contactFilter, hitBuffer, vertical.magnitude * magnitudeAdjustment) <= 0) rb2d.position += vertical;
    }

    public Vector2 getMovementDir()
    {
        return movementDir;
    }
}
