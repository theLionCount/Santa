using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour, IDmgSource
{
    public Vector3 dir;
    public float v;
    public GameObject onHit;
    public float dmg;
    public float popDmg;

    public GameObject cantHit;

    public int pop;
    public float secondaryDmg;
    public GameObject secondaryBullet;
    public float range;
    public float secondaryRangeinTiles;

    public bool secondaryTarget;
    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Walls");
    }

    private void FixedUpdate()
    {
        transform.position += dir * v;
        range--;
        if (range <= 0) OnCollisionEnter2D(null);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null || !collision.gameObject == cantHit)
        {
            Destroy(gameObject);
            Instantiate(onHit, transform.position, Quaternion.identity, null);
            if (pop > 0)
            {
                for (int i = 0; i < pop; i++)
                {
                    var angle = Random.Range(0, 360f);

                    var blt = Instantiate(secondaryBullet, transform.position, Quaternion.Euler(0, 0, angle)).GetComponent<Bullet>();
                    blt.dir = Quaternion.Euler(0, 0, angle) * Vector3.right;
                    blt.cantHit = collision.gameObject.transform.parent.gameObject;
                    blt.pop = 0;
                    blt.dmg = popDmg;
                    blt.range *= Random.Range(0.7f, 1.1f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float hitTarget(IDmgTarget target)
    {
        if (secondaryTarget)
        {
            var enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy")).Where(t=> Physics2D.Linecast(transform.position, t.transform.position, layerMask).collider == null).ToList();
            enemies.Remove(((MonoBehaviour)target).gameObject);
            if (enemies.Count > 0)
            {
                var trgt = enemies.OrderBy(t => (t.transform.position - transform.position).magnitude).First();
                if ((trgt.transform.position - transform.position).magnitude <= secondaryRangeinTiles)
                {

                    var angle = Vector3.SignedAngle(new Vector3(1, 0, 0), trgt.transform.position - transform.position, new Vector3(0, 0, 1));

                    var blt = Instantiate(secondaryBullet, transform.position, Quaternion.Euler(0, 0, angle)).GetComponent<Bullet>();
                    blt.dir = Quaternion.Euler(0, 0, angle) * Vector3.right;
                    blt.cantHit = ((MonoBehaviour)target).gameObject;
                    blt.pop = 0;
                    blt.secondaryTarget = false;
                    blt.dmg = secondaryDmg;
                    blt.range *= 1.5f;
                }
            }
        }
        return (((MonoBehaviour)target).gameObject != null && ((MonoBehaviour)target).gameObject != cantHit) ? dmg : 0;
    }

    public float getDamage()
    {
        return dmg;
    }
}
