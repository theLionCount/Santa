using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualReload : Reloader
{
    public GameObject reloadObject;
    GameObject go;

    public float rldSpeed;

    protected override void actualStartReload()
    {
        go = Instantiate(reloadObject, transform.parent);
        go.GetComponent<ReloadBar>().visual = this;
        go.GetComponent<Animator>().speed = rldSpeed;
    }

    public override void cancelReload()
    {
        base.cancelReload();
        Destroy(go);
    }
}
