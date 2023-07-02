using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class ResizableTilemap
{
    [SerializeField] int[] _tiles = { -1 };
    [SerializeField] private uint _extension;

    private void Resize(uint newExtension)
    {
        var newSize = newExtension * 2 + 1;
        var newTiles = Enumerable.Repeat(-1, (int)(newSize * newSize)).ToArray();

        var currentSize = _extension * 2 + 1;
        var minSize = Mathf.Min((int)currentSize, (int)newSize);
        var halfMinSize = minSize / 2;

        for (int y = -halfMinSize; y < halfMinSize; y++)
        {
            for (int x = -halfMinSize; x < halfMinSize; x++)
            {
                newTiles[(x + (newSize / 2)) + newSize * (y + (newSize / 2))] = GetTile(x, y);
            }
        }

        _extension = newExtension;
        _tiles = newTiles;
    }

    public int GetTile(int x, int y)
    {
        if (Mathf.Abs(x) > _extension || Mathf.Abs(y) > _extension)
            return -1; // return -1 when outside of grid.

        var size = _extension * 2 + 1;
        return _tiles[(x + _extension) + size * (y + _extension)];
    }

    public void SetTile(int x, int y, int tileID)
    {
        if (Mathf.Abs(x) > _extension || Mathf.Abs(y) > _extension)
            Resize((uint)Mathf.Abs(x));
        
        var size = _extension * 2 + 1;
        _tiles[(x + _extension) + size * (y + _extension)] = tileID;
    }

    public uint GetExtension()
    {
        return _extension;
    }
}
