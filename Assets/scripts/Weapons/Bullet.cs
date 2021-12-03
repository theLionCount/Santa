using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 dir;
    public float v;
    public GameObject onHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position += dir * v;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(onHit, transform.position, Quaternion.identity, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
