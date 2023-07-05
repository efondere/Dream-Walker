using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacePreviewTileManager: MonoBehaviour
{
    [SerializeField] private TileBase[] _tiles;

    public TileBase GetTile(int index)
    {
        return _tiles[index];
    }
}
