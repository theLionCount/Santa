using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rot = Vector2.SignedAngle(new Vector2(0, 1), target.transform.position - transform.position);
        transform.rotation = Quaternion.Euler(0,0,rot);
    }

    public void setEnemy(GameObject enemy)
    {
        target = enemy;
    }
}
