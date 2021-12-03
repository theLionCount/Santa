using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    protected Vector3 target;
    public Vector3 dir;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        dir = Vector3.zero;
    }

    private void FixedUpdate()
    {
        var rotation = Vector3.SignedAngle(new Vector3(-1, 0, 0), dir, new Vector3(0, 0, 1));
        angle = rotation;

        if (dir.x < 0) transform.localScale = new Vector3(-transform.parent.localScale.x, 1, transform.localScale.z);
        else transform.localScale = new Vector3(-transform.parent.localScale.x, -1, transform.localScale.z);

        transform.eulerAngles = new Vector3(0, 0, rotation);
    }

}
