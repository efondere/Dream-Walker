using UnityEngine;

public class PlaceablePrefab : Placeable
{
    [SerializeField] public GameObject prefab;
    // useful for moving blocks
    // if activated, it won't place any tiles from the place preview and instead puts them all to the
    // placeholderpreviewer
    [SerializeField] private bool _usePlaceholderTiles;

    public override bool OnPlace(Vector3Int position)
    public delegate void onPlace(GameObject tile);
    public static event onPlace onPlaceEvent;
    // added rotation
    public override bool OnPlace(Vector3Int position)
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

                if (tileID < -1)
                {
                    _tilemapManager.PlacePlaceholderTile(pos, tileID);
                }
                else
                {
                    if (_usePlaceholderTiles)
                        _tilemapManager.PlacePlaceholderTile(pos, -2);
                    else
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
