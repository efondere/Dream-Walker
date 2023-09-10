using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBlock : MonoBehaviour
{

    // SUMMARY: script attached on each zone tile to keep track of how many zone tiles are colliding with a tile
    ZoneBehavior zoneBehavior;
    bool canCollide = true;

    private void Start()
    {
        zoneBehavior = GetComponentInParent<ZoneBehavior>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canCollide)
        {
            zoneBehavior.nbTriggeredZoneTiles++;
            canCollide = false;
        }
        StartCoroutine(Reset());

    }
    public IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        canCollide = true;
    }


}
