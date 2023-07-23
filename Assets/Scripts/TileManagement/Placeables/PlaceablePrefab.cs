using UnityEngine;

public class PlaceablePrefab : Placeable
{
    [SerializeField] public GameObject prefab;

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
                var tileID = tilePreview.grid.GetTile(i, j);
                
                if (tileID == -1)
                    continue;

                var pos = new Vector3Int(position.x + i, position.y + j, position.z);
                
                _tilemapManager.PlacePlaceholderTile(pos, tileID);
                
                if (tileID >= 0)
                {
                    _tilemapManager.PlaceSolidTile(pos, tilePreview.tiles[tileID]);
                }
            }
        }

        Instantiate(prefab, _tilemapManager.CellToWorld(position), Quaternion.identity);

        return true;
    }
}
