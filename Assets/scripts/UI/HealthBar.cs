using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image bar;
    Character player;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
        player = GameObject.Find("Player").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = player.health / player.maxHealth;
        text.text = "HP: " + ((int)player.health).ToString() + "/" + ((int)player.maxHealth).ToString();
    }
}
