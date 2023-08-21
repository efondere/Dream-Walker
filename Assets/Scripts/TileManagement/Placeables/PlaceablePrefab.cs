using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceablePrefab : Placeable
{
    [SerializeField] public GameObject prefab;
    public delegate void onPlace(GameObject tile);
    public static event onPlace onPlaceEvent;
    // added rotation
    public override bool OnPlace(Vector3Int position, int rotation = 0)
    {
        var gridExtension = tilePreview.grid.GetExtension();

        for (int i = -(int)gridExtension; i <= gridExtension; i++)
        {
            for (int j = -(int)gridExtension; j <= gridExtension; j++)
            {
                if (tilePreview.grid.GetTile(i, j) == -1)
                    continue;

                var pos = new Vector3Int(position.x + i, position.y + j, position.z);
                if (_tilemapManager.IsColliding(pos))
                {
                    return false;
                }
            }
        }

        for (int i = -(int)gridExtension; i <= gridExtension; i++)
        {
            for (int j = -(int)gridExtension; j <= gridExtension; j++)
            {
                // add rotation for testing
                var tileID = tilePreview.GetTile(i, j, rotation);
                
                if (tileID == -1)
                    continue;

                var pos = new Vector3Int(position.x + i, position.y + j, position.z);
                
                if (tilePreview.grid.GetTile(i, j) < -1)
                {
                    _tilemapManager.PlacePlaceholderTile(pos, tileID);
                }
                else
                {
                    _tilemapManager.PlaceSolidTile(pos, tilePreview.tiles[tileID]);
                }
            }
        }

        // add rotation for testing
        GameObject instance = Instantiate(prefab, _tilemapManager.CellToWorld(position), Quaternion.identity);
        instance.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, rotation * -90f);
        onPlaceEvent?.Invoke(instance);




        return true;
    }
}
