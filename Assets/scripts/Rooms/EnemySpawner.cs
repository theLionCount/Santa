using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject tp;

    Room room;
    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        room = GetComponentInParent<Room>();
    }

    public void instantiate(GameObject e)
    {
        enemy = e;
        var t = Instantiate(tp, transform.position, Quaternion.identity);
        t.GetComponent<TPIn>().spawner = this;
    }

    public void tpReady()
    {
        room.addLivingEnemy(Instantiate(enemy, transform.position, Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
