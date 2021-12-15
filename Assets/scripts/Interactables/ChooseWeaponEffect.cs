using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseWeaponEffect : InteractEffect
{

    public static List<string> weaponNames = new List<string>
    { 
        "Pistol",
        "Blaster"
    };

    public static List<string> rewardNames = new List<string>
    {
        "BlasterTriggerMod",
        "BlasterAmmo",
        "BlasterMag",
        "PistolBullet",
        "PistolHammer",
        "PistolMag",
    };

    public string weaponName;
    

    public override void doEffect()
    {
        foreach (var item in weaponNames)
        {
            playerObject.transform.Find(item).gameObject.SetActive(false);
        }
        foreach (var item in rewardNames)
        {
            Room.possibleRewards.Remove(item);
        }
        var weaponObj = playerObject.transform.Find(weaponName).gameObject;
        weaponObj.SetActive(true);

        playerChar.weaponController = weaponObj.GetComponent<WeaponController>();
        playerRollMovement.weaponController = weaponObj.GetComponent<WeaponController>();

        foreach (var item in rewardNames.Where(t=>t.StartsWith(weaponName)))
        {
            Room.possibleRewards.Add(item);
        }
        transform.parent.parent.parent.GetComponent<Room>().setDoors(true);
    }
}
