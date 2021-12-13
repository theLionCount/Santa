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

    ProgressTracker progress;

    public string shortName;

    // Start is called before the first frame update
    void Start()
    {
        progress = GameObject.Find("Progress").GetComponent<ProgressTracker>();
        playerObject = GameObject.Find("Player");
        playerChar = playerObject.GetComponent<Character>();
        playerWeapon = playerObject.GetComponentInChildren<MagazinedWeapon>();
        weaponController = playerObject.GetComponentInChildren<CharacterWeaponController>();
        reload = playerWeapon.GetComponent<VisualReload>();
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
        return "press F";
    }

    public virtual string getDoorPrompt()
    {
        return "some goodie";
    }
}
