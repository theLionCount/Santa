using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    Animator animator;
    bool opened;
    bool travelable;
    public string reward;
    public string rewardPrompt;

    public int distance;

    RoomSelector roomSelector;

    public Text prompt;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        prompt.text = "";
        roomSelector = GameObject.Find("GamePlay").GetComponent<RoomSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (opened && travelable && Input.GetKeyDown(KeyCode.F))
        {
            roomSelector.changeRoom(reward, distance-1);
        }
    }

    public void open()
    {
        animator.SetBool("Open", true);
        
    }

    public void openFinished()
    {
        opened = true;
        prompt.text = distance > 1 ? (rewardPrompt + " after " + (distance - 1).ToString() + " rooms") : rewardPrompt;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        travelable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        travelable = false;
    }

}
