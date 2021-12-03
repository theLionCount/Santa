using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedReload : Reloader
{
    public int reloadTime;
    int cd;

    private void FixedUpdate()
    {
        if (reloadInProgress) 
        {
            cd--;
            if (cd <= 0) finishReload();
        }
        else cd = reloadTime;
    }

    public override void cancelReload()
    {
        cd = reloadTime;
    }
}
