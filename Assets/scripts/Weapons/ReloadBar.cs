using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBar : MonoBehaviour
{
    public VisualReload visual;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.parent.localScale.x, 1, 1);
    }

    public void finishReload()
    {
        Destroy(gameObject);
        visual.finishReload();
    }
}
