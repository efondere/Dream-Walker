using System.Collections.Generic;
using UnityEngine;

public class PlaceholderPreviewer : MonoBehaviour
{
    private class InstantiatedPS
    {
        public GameObject gameObject; // come to think of it, this works with any time of GO!
        public int id = -1;
        public Vector2Int position;

        public void Clear()
        {
            id = -1;
            Destroy(gameObject);
        }
    }

    [SerializeField] private GameObject[] _particleSystemPrefabs;
    private ResizableTilemap _tiles = new();
    private Grid _grid;
    private List<InstantiatedPS> _instantiatedPSes = new();
    private bool _isRendering;

    void Start()
    {
        _grid = GetComponentInParent<Grid>();
        SetRendering(true);
    }

    public int GetTile(int x, int y)
    {
        return _tiles.GetTile(x, y);
    }
    
    public void SetTile(int x, int y, int tileID)
    {
        _tiles.SetTile(x, y, tileID);

        if (_isRendering)
        {
            UpdateRenderer(new Vector2Int(x, y));
        }
    }

    public void EmptyTile(int x, int y)
    {
        SetTile(x, y, -1);
    }

    private void ClearParticleSystems()
    {
        foreach (var ps in _instantiatedPSes)
        {
            ps.Clear();
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
            ClearParticleSystems();
        }
    }

    private void UpdateRenderer(Vector2Int position)
    {
        // 1) Determine if there is already a PS at that position
        // A) if there is none (either GetPSAtPosition returned -1 or that PS has id of -1):
        // A.1)  do nothing if the tileid at this position is -1
        // A.2)  create a new particle System (use GetNextAvailablePS) at that position.
        // B) if there is one
        // B.1)  clear it if the tileID at this position is -1
        // B.2)  clear it and replace it if the tileID at that position is not the same as the pre-existing PS
        // B.3)  do nothing if both ids match. (THIS IS WHAT WILL PREVENT THEM FROM DISAPPEARING!!!)
        var currentID = GetTile(position.x, position.y);
        
        var existingPS = GetPSAtPosition(position);
        if (existingPS == -1)
        {
            if (currentID == -1)
                return;

            var id = GetNextAvailablePS();
            _instantiatedPSes[id].position = position;
            _instantiatedPSes[id].id = currentID;
            var pos = _grid.CellToWorld(new Vector3Int(position.x, position.y, 0)) + new Vector3(0.5f, 0.5f, 0);
            _instantiatedPSes[id].gameObject = Instantiate(_particleSystemPrefabs[currentID - 2], pos, Quaternion.identity);
        }
        else if (_instantiatedPSes[existingPS].id == -1)
        {
            if (currentID == -1)
                return; // assume we only have one dead prefab per position, we could also (somehow) reset position in Clear()

            _instantiatedPSes[existingPS].id = currentID;
            var pos = _grid.CellToWorld(new Vector3Int(position.x, position.y, 0)) + new Vector3(0.5f, 0.5f, 0);
            _instantiatedPSes[existingPS].gameObject = Instantiate(_particleSystemPrefabs[currentID], pos, Quaternion.identity);
        }
        else
        {
            if (currentID == -1)
            {
                _instantiatedPSes[existingPS].Clear();
                return;
            }

            if (_instantiatedPSes[existingPS].id == currentID)
                return;
            
            _instantiatedPSes[existingPS].Clear();
            _instantiatedPSes[existingPS].id = currentID;
            var pos = _grid.CellToWorld(new Vector3Int(position.x, position.y, 0)) + new Vector3(0.5f, 0.5f, 0);
            _instantiatedPSes[existingPS].gameObject = Instantiate(_particleSystemPrefabs[currentID], pos, Quaternion.identity);
        }
    }
    
    private void UpdateRenderer()
    {
        var size = 1 + 2 * _tiles.GetExtension();

        for (int y = -(int)_tiles.GetExtension(); y <= _tiles.GetExtension(); y++)
        {
            for (int x = -(int)_tiles.GetExtension(); x <= _tiles.GetExtension(); x++)
            {
                if (GetTile(x, y) != -1)
                {
                    UpdateRenderer(new Vector2Int(x, y));
                }
            }
        }
    }

    // looks through the list to find a disabled PS. If there is none, it will create a new one.
    // returns the index of the PS in the list
    private int GetNextAvailablePS()
    {
        for (var i = 0; i < _instantiatedPSes.Count; i++)
        {
            if (_instantiatedPSes[i].id == -1)
            {
                return i;
            }
        }
        
        _instantiatedPSes.Add(new InstantiatedPS());
        return _instantiatedPSes.Count - 1;
    }

    // Find a particle system in the list at the given position
    // return the index or -1 if there is none
    // note: we assume that there is only one ps at a given position
    private int GetPSAtPosition(Vector2Int position)
    {
        for (var i = 0; i < _instantiatedPSes.Count; i++)
        {
            if (_instantiatedPSes[i].position == position)
            {
                return i;
            }
        }

        return -1;
    }
}
