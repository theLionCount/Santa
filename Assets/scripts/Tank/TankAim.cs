using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAim : Aiming
{
    GameObject player;

    public float arc;

    Vector3 baseDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void setInitialTarget()
    {
        target = player.transform.position;
        target.z = 0;
        dir = target - transform.parent.position;
        baseDir = dir;
    }

    public void setFireAim(float rot)
    {
        dir = Quaternion.Euler(0, 0, arc * rot) * baseDir;
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
}
