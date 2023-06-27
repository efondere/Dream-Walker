using DG.Tweening.Plugins;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

class ResizableTilemap
{
    private int[] _tiles;
    private int _size;

    public ResizableTilemap()
    {
        Resize(3); // start with 5*5 grid
    }

    private void Resize(int newSize)
    {
        Assert.IsTrue(newSize >= 0, "Expected a positive size for ResizableGrid.");
        Assert.IsTrue(newSize % 2 != 0, "Expected an odd size for ResizableGrid.");

        if (_tiles == null)
        {
            _size = newSize;
            _tiles = Enumerable.Repeat(-1, _size * _size).ToArray();
        }

        var newTiles = Enumerable.Repeat(-1, newSize * newSize).ToArray() as int[];

        var minSize = Mathf.Min(_size, newSize);
        var halfMinSize = minSize / 2;
       
        for (int y = -halfMinSize; y < halfMinSize; y++)
        {
            for (int x = -halfMinSize; x < halfMinSize; x++)
            {
                newTiles[(x + (newSize / 2)) + newSize * (y + (newSize / 2))]  = GetTile(x, y);
            }
        }

        _size = newSize;
        _tiles = newTiles;
    }

    public int GetTile(int x, int y)
    {
        if (x > _size / 2 || x < -_size / 2 || y > _size / 2 || y < -_size / 2)
            return -1; // return -1 when outside of grid.

        return _tiles[(x + (_size / 2)) + _size * (y + (_size / 2))];
    }

    public void SetTile(int x, int y, int tileID)
    {
        if (x > _size / 2 || x < -_size / 2 || y > _size / 2 || y < -_size / 2)
            Resize(x * 2 + 1);

        _tiles[(x + (_size / 2)) + _size * (y + (_size / 2))] = tileID;
    }
}

public class PlaceholderPreviewer : MonoBehaviour
{
    private ResizableTilemap _tiles;
    private Grid _grid;
    private GameObjectPool _particleSystems;
    private bool _isRendering;

    void Start()
    {
        _grid = GetComponentInParent<Grid>();
    }

    public int GetTile(int x, int y)
    {
        return _tiles.GetTile(x, y);
    }
    
    public void SetTile(int x, int y, int tileID)
    {
        Assert.IsTrue((tileID >= 0), "Expected a positive TileID for placeholder.");

        _tiles.SetTile(x, y, tileID);

        if (_isRendering)
        {
            UpdateRenderer();
        }
    }

    public void EmptyTile(int x, int y)
    {
        _tiles.SetTile(x, y, -1);

        UpdateRenderer();
    }

    public void setRendering(bool rendering)
    {
        _isRendering = rendering;

        if (_isRendering)
        {
            UpdateRenderer();
        }
    }

    void UpdateRenderer()
    {
        throw new NotImplementedException();
    }
}
