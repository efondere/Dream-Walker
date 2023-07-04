using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    public TilePreview tilePreview;
    public TilemapManager tilemapManager; // TODO: make this static?

    public virtual bool OnPlace(Vector3Int position)
    {
        return false;
    }
}
