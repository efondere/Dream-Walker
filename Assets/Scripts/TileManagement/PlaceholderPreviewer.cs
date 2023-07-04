using DG.Tweening.Plugins;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

public class PlaceholderPreviewer : MonoBehaviour
{
    // tmp: using SerializeField for testing here
    private ResizableTilemap _tiles = new ResizableTilemap();
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
            UpdateRenderer(); // optimization: send only the position that needs to be updated in here.
        }
    }

    public void EmptyTile(int x, int y)
    {
        _tiles.SetTile(x, y, -1);

        if (_isRendering)
        {
            UpdateRenderer();
        }
    }

    public void SetRendering(bool rendering)
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
