using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    AimingRollinCharacter aim;
    ScreenShakeController screenShake;
    RollMovementBAse roll;

    public float xAimFollow, yAimFollow;

    // Start is called before the first frame update
    void Start()
    {
        aim = transform.parent.GetComponent<AimingRollinCharacter>();
        roll = transform.parent.GetComponent<RollMovementBAse>();
        screenShake = GameObject.Find("GamePlay").GetComponent<ScreenShakeController>();
    }


    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3((aim.aim.x * xAimFollow) * transform.parent.localScale.x, aim.aim.y * yAimFollow, transform.localPosition.z) + screenShake.curentShake;   

        transform.localScale = new Vector3(transform.parent.localScale.x, 1, 1);
    }
}
