using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePreview : MonoBehaviour
{

    [SerializeField] public TileBase[] tiles;
    [SerializeField] public ResizableTilemap grid;

    public int GetTile(int x, int y, int rotation)
    {
        return grid.GetTile((float)rotation % 2 != 0 ? (y * (int)Mathf.Pow(-1, (float)(rotation / 2))) : (x * (int)Mathf.Pow(-1, (float)rotation/2)),
            (float)rotation % 2 != 0 ? (x * (int)Mathf.Pow(-1, 1 + rotation/2)) : (y * (int)Mathf.Pow(-1, (float)rotation/2)));

    }

    // grid indices:
    // -1 : no tile (air)
    // <= -2: placeholder / clearance tile. We will support values other than just -2 for various types of placeholder tiles (ie. colors?)
    // >= 0: various tiles from the tiles array.
}
