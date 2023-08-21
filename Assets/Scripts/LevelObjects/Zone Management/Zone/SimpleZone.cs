using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// simple zone is any zone that triggers a behavior when a single block is placed on it 
public class SimpleZone : ZoneBehavior
{
    public override void RespondToTrigger(GameObject go)
    {
        if (go.transform.childCount == nbTriggeredZoneTiles && canPlace)
        {
            Debug.Log("Simple Zone Completed");
            onPlaced.Invoke();
        }
        else
        {
            Debug.Log("Remove block");
            canPlace = false;
        }
    }

    public override void OnBlockRemoved()
    {
            
    }
}
