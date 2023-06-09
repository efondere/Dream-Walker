using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    public TilePreview tilePreview;

    public virtual bool OnPlace(Vector3Int position, GridLayout grid)
    {
        return false;
    }
}
