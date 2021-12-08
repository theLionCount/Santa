using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAim : Aiming
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;
        dir = target - transform.parent.position;
    }
}
