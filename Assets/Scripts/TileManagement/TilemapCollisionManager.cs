using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCollisionManager : MonoBehaviour
{
    [SerializeField] private Tilemap[] _solidTilemaps;

    public bool isColliding(Vector3Int pos)
    {
        for (int i = 0; i < _solidTilemaps.Length; i++) {
            if (_solidTilemaps[i].GetTile(pos) != null)
            {
                return true;
            }
        }

        return false;
    }
}
