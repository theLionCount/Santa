using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCharacterBase : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask.GetMask("Walls"));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
