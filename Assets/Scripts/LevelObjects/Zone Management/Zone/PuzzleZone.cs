using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleZone : ZoneBehavior
{
    int lastNbTriggeredZoneTiles = 0;
    public override void RespondToTrigger(GameObject go)
    {
        var spriteContainer = transform.Find("Sprites");
        if (go.transform.childCount > nbTriggeredZoneTiles - lastNbTriggeredZoneTiles)
        {
            Debug.Log("Remove Block from Puzzle");
            Camera.main.DOShakePosition(0.7f);
            canPlace = false;
        }
        else if (nbTriggeredZoneTiles == spriteContainer.childCount)
        {
            Debug.Log("Puzzle Success");
        }
        lastNbTriggeredZoneTiles = nbTriggeredZoneTiles;
        Debug.Log(spriteContainer.childCount);
    }


    public override void OnBlockRemoved()
    {

    }
}