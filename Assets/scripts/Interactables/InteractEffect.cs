using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractEffect : MonoBehaviour
{
    protected GameObject playerObject;
    protected Character playerChar;
    protected MagazinedWeapon playerWeapon;
    protected CharacterWeaponController weaponController;
    protected VisualReload reload;
    protected HealthTracker health;


    protected ProgressTracker progress;


    public string shortName;
    public string doorPrompt;
    public string promtp;

    // Start is called before the first frame update
    public virtual void Start()
    {
        progress = GameObject.Find("Progress").GetComponent<ProgressTracker>();
        playerObject = GameObject.Find("Player");
        playerChar = playerObject.GetComponent<Character>();
        playerWeapon = playerObject.GetComponentInChildren<MagazinedWeapon>();
        weaponController = playerObject.GetComponentInChildren<CharacterWeaponController>();
        reload = playerWeapon.GetComponent<VisualReload>();
        health = playerChar.GetComponent<HealthTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void doEffect()
    {
        if (!progress.upgrades.ContainsKey(shortName)) progress.upgrades.Add(shortName, 0);
        progress.upgrades[shortName]++;
    }

    public virtual string getPrompt()
    {
        return "Press F to pick up!\r\n" + promtp; ;
    }

    public virtual string getDoorPrompt()
    {
        return doorPrompt;
    }
}
