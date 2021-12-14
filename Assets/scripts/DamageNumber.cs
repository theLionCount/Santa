using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    Text text;
    public Vector3 fadeOutV;
    public float fadeoutTime;
    float cd;
    bool fading;

    public float maxSize;
    public float dmgAtMaxSize;
    public float dmgAtMinSize;

    float textBaseSize;

    float pulseSize;
    float normalSize;

    bool message;

    public void setMessage(string message)
    {
        if (text == null)
        {
            text = GetComponent<Text>();
            textBaseSize = text.transform.localScale.x;
        }
        text.text = message;
        normalSize = 1;
        text.transform.localScale = new Vector3(textBaseSize, textBaseSize, 1);
        text.color = new Color(1, 1, 0);
        pulseSize = 1.1f;
    }

    public void setDamage(float dmg, bool crit)
    {
        if (text == null)
        {
            text = GetComponent<Text>();
            textBaseSize = text.transform.localScale.x;
        }
        text.text = ((int)dmg).ToString();
        if (crit) text.text += " CRIT";
        var range = (dmgAtMaxSize - dmgAtMinSize);
        var x = (dmg > dmgAtMinSize ? dmg : dmgAtMinSize) - dmgAtMinSize;
        var point = x / range;
        if (point > 1) point = 1;
        var size = 1 + ((maxSize - 1) * point);
        normalSize = size;
        text.transform.localScale = new Vector3(textBaseSize * size * pulseSize, textBaseSize * size * pulseSize, 1);
        text.color = new Color(1, 1 - point, 1 - point);
        pulseSize = 1.1f;
    }

    public void fadeOut()
    {
        cd = fadeoutTime;
        fading = true;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {

        if (pulseSize > 1) pulseSize -= 0.01f;
        text.transform.localScale = new Vector3(textBaseSize * normalSize * pulseSize, textBaseSize * normalSize * pulseSize, 1);
        if (fading)
        {
            cd--;
            text.transform.localPosition += fadeOutV;
            text.color = new Color(text.color.r, text.color.b, text.color.g, cd / fadeoutTime);
            if (cd <= 0) Destroy(gameObject);
        }
    }


}
