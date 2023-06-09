using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Trap Tile", menuName = "Tiles/TrapTile")]

public class TrapTile : Tile
{
    public float damage;

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        Debug.Log(damage);
    }
}
