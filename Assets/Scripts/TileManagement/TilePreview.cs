using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileGrid
{
    [System.Serializable]
    public struct rowData
    {
        public int[] row;
    }
    public rowData[] rows = new rowData[10];

    public int At(int i, int j)
    {
        return rows[j].row[i];
    }
}

public class TilePreview : MonoBehaviour
{
    [SerializeField] public Tile[] tiles;
    [SerializeField] public TileGrid grid;
}
