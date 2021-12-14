using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    bool pickable;

    GameObject player;

    protected InteractEffect effect;

    protected Room room;

    public Text prompt;

    public GameObject other;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        effect = GetComponent<InteractEffect>();
        room = GameObject.FindGameObjectWithTag("Room").GetComponent<Room>();
        prompt.text = effect.getPrompt();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickable && Input.GetKeyDown(KeyCode.F)) pickUp();
    }

    public virtual void pickUp()
    {
        effect.doEffect();
        room.rewardTaken();
        Destroy(gameObject);
        if (other!=null) Destroy(other);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        prompt.gameObject.SetActive(true);
        pickable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        prompt.gameObject.SetActive(false);
        pickable = false;
    }
}
