using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    float currentMagintude;
    public float dampening;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentMagintude *= dampening;
        if (currentMagintude <= 1f / 16f) currentMagintude = 0;
    }

    public void shake(float magnitude)
    {
        if (currentMagintude < magnitude) currentMagintude = magnitude;
    }

    public Vector3 curentShake => new Vector3(Random.Range(-currentMagintude, currentMagintude), Random.Range(-currentMagintude, currentMagintude), 0);
}
