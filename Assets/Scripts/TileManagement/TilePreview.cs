using DG.Tweening.Plugins;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileGrid
{
    [System.Serializable]
    public struct RowData
    {
        public int[] row;
    }
    public RowData[] rows;

    public int At(int i, int j)
    {
        return rows[j].row[i];
    }
}

[System.Serializable]
public struct TileGrid_
{
    [SerializeField]
    public int[][] _data;

    [SerializeField]
    private int test;

    public readonly int At(int i, int j)
    {
        return _data[i][j];
    }
}

public class TilePreview : MonoBehaviour
{
    [SerializeField] public TileBase[] tiles;
    [SerializeField] public TileGrid grid;
}