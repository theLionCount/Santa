using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloader : MonoBehaviour
{
    protected MagazinedWeapon weapon;
    public bool reloadInProgress;


    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<MagazinedWeapon>();
    }

    public virtual void startReload()
    {
        if (!reloadInProgress)
        {
            reloadInProgress = true;
            actualStartReload();
        }
    }

    protected virtual void actualStartReload()
    { 
    }

    public virtual void finishReload()
    {
        reloadInProgress = false;
        weapon.finishReload();
    }

    public virtual void cancelReload()
    {
        reloadInProgress = false;
    }
}
