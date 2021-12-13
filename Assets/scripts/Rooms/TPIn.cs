using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPIn : MonoBehaviour
{
    GameObject strobeLight;
    public EnemySpawner spawner;
    
    // Start is called before the first frame update
    void Start()
    {
        strobeLight = transform.Find("strobeLight").gameObject;
    }

    public void lightOn()
    {
        strobeLight.SetActive(true);
    }

    public void lightOff()
    {
        strobeLight.SetActive(false);
    }

    public void rdy()
    {
        spawner.tpReady();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
