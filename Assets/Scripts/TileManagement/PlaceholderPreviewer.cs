using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(GameObjectPool))]
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
        _particleSystems = GetComponent<GameObjectPool>();
        SetRendering(true);
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
        else
        {
            _particleSystems.ClearAll();
        }
    }

    void UpdateRenderer()
    {
        //TODO: fix the following line so that particles don't get reset on accident.
        _particleSystems.ClearAll();
        
        var size = 1 + 2 * _tiles.GetExtension();

        for (int y = -(int)_tiles.GetExtension(); y <= _tiles.GetExtension(); y++)
        {
            for (int x = -(int)_tiles.GetExtension(); x <= _tiles.GetExtension(); x++)
            {
                if (GetTile(x, y) != -1)
                {
                    var pos = _grid.CellToWorld(new Vector3Int(x, y, 0)) + new Vector3(0.5f, 0.5f, 0);
                    _particleSystems.Instantiate(pos, Quaternion.identity);
                }
            }
        }
    }
}
