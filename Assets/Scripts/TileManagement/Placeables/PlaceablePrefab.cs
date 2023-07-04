using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceablePrefab : Placeable
{
    [SerializeField] public Tilemap tileset;
    [SerializeField] public Tilemap placeholderPreview;
    [SerializeField] public GameObject prefab;
    [SerializeField] public AnimatedTile placeholderTile;

    public override bool OnPlace(Vector3Int position)
    {
        var gridExtension = tilePreview.grid.GetExtension();

        for (int i = -(int)gridExtension; i <= gridExtension; i++)
        {
            for (int j = -(int)gridExtension; j <= gridExtension; j++)
            {
                if (tilePreview.grid.GetTile(i, j) == -1) // -1 is no tile, 0 is air visualization
                    continue;

                var pos = new Vector3Int(position.x + i, position.y + j, position.z);
                if (tilemapManager.IsColliding(pos))
                {
                    return false;
                }
            }
        }

        for (int i = -(int)gridExtension; i <= gridExtension; i++)
        {
            for (int j = -(int)gridExtension; j <= gridExtension; j++)
            {
                if (tilePreview.grid.GetTile(i, j) < 0)
                    continue;

                var pos = new Vector3Int(position.x + i, position.y + j, position.z);
                
                if (tilePreview.grid.GetTile(i, j) == 0)
                {
                    placeholderPreview.SetTile(pos, placeholderTile);
                }
                else
                {
                    var tile = tilePreview.tiles[tilePreview.grid.GetTile(i, j)];
                    placeholderPreview.SetTile(pos, tile);
                }
            }
        }

        Instantiate(prefab, tilemapManager.CellToWorld(position), Quaternion.identity);

        return true;
    }
}
