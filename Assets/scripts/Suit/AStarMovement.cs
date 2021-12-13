using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarMovement : MonoBehaviour
{
    IMovable character;
    RouteProvider rp;
    public float targetPrecission;

    public Vector3 targetPos { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<IMovable>();
        rp = new RouteProvider();
    }

    public void move()
    {
        var target = rp.nextTarget(transform.position, targetPos) + new Vector2(0.5f, 0.5f);
        Debug.DrawLine(transform.position, new Vector3(target.x, target.y, 0), Color.blue);

        Vector2 v = Vector2.zero;
        if (target.y > transform.position.y + 0.1f) v.y += 1;
        if (target.y < transform.position.y - 0.1f) v.y -= 1;
        if (target.x > transform.position.x + 0.1f) v.x += 1;
        if (target.x < transform.position.x - 0.1f) v.x -= 1;
        character.move(v);
    }

    public void stop()
    {
        character.move(Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isPointOk(Vector3 pos)
    {
        return rp.isPointOk(pos);
    }

    public bool atTarget()
    {
        return (transform.position  - targetPos).magnitude < targetPrecission;
    }
}
