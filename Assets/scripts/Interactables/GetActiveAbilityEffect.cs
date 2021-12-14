using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetActiveAbilityEffect : InteractEffect
{ 
    public static List<string> abilities = new List<string>
    {
        "BulletTime",
        "Rampage",
        "Clear",
    };

    public string selected;
    ActiveAbilities aa;

    public override void Start()
    {
        base.Start();
        //selected = abilities[Random.Range(0, abilities.Count)];
        aa = playerObject.GetComponent<ActiveAbilities>();
    }

    public override void doEffect()
    {
        aa.selectedAbility = selected;
        aa.charges = 1;
        aa.maxCharges = 1;
        progress.activeAbility = selected;
        progress.activeAbilityCharges = 1;
    }

    public override string getPrompt()
    {
        if (selected == "BulletTime") return "Bullet time - slows down time for a short period of time";
        else if (selected == "Rampage") return "Rampage - shoot faster, and forget about reloads";
        else if (selected == "Clear") return "Active countermeasures - clears the map of any pesky enemy projectiles";
        else return "idunno";
    }

    public override string getDoorPrompt()
    {
        return "Active ability";
    }

}
