using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{

    public Dictionary<string, int> upgrades = new Dictionary<string, int>();
    public int roomNum;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Room: " + roomNum.ToString() + "\r\n";
        text.text += "Upgrades: " + "\r\n";
        foreach (var item in upgrades.Keys)
        {
            text.text += item + " x" + upgrades[item] + "\r\n";
        }
    }
}
