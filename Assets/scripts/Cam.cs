using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    Aiming aim;
    ScreenShakeController screenShake;

    public float xAimFollow, yAimFollow;

    // Start is called before the first frame update
    void Start()
    {
        aim = transform.parent.GetComponentInChildren<Aiming>();
        screenShake = GameObject.Find("GamePlay").GetComponent<ScreenShakeController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3((aim.dir.x * xAimFollow) * transform.parent.localScale.x, aim.dir.y * yAimFollow, transform.localPosition.z) + screenShake.curentShake;
        transform.localScale = new Vector3(transform.parent.localScale.x, 1, 1 );
    }
}
