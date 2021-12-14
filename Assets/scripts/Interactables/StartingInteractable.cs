using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingInteractable : Interactable
{
    public override void pickUp()
    {
        effect.doEffect();
        room.rewardTaken();
    }
}
