using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image bar;
    HealthTracker health;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
        health = GameObject.Find("Player").GetComponent<HealthTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = health.health / health.maxHealth;
        text.text = "HP: " + ((int)health.health).ToString() + "/" + ((int)health.maxHealth).ToString();
    }
}
