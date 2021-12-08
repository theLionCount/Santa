using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitAimSlow : Aiming
{
    GameObject player;
    int t;
    Vector3[] playerPos;

    public int delay;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerPos = new Vector3[delay];
        for (int i = 0; i < delay; i++)
        {
            playerPos[i] = player.transform.position;
        }
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        t++;
        t %= delay;
        target = playerPos[t];
        playerPos[t] = player.transform.position;
        target.z = 0;
        dir = target - transform.parent.position;
    }
}
