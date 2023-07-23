using UnityEngine;

public class PlaceableTile : Placeable
{
    public override bool OnPlace(Vector3Int position)
    {
        var gridExtension = tilePreview.grid.GetExtension();

        for (int i = -(int)gridExtension; i <= gridExtension; i++)
        {
            for (int j = -(int)gridExtension; j <= gridExtension; j++)
            {
                if (tilePreview.GetTile(i, j) == -1)
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
                var tileID = tilePreview.GetTile(i, j);
                if (tileID == -1)
                    continue;
                
                var pos = new Vector3Int(position.x + i, position.y + j, position.z);
                
                if (tileID < -1)
                {
                    _tilemapManager.PlacePlaceholderTile(pos, tileID);
                }
                else
                {
                    _tilemapManager.PlaceSolidTile(pos, tilePreview.tiles[tileID]);
                }
            }
        }

        return true;
    }

    public override bool Rotate(bool clockwise)
    {
        tilePreview.direction = TilePreview.GetNextDirection(tilePreview.direction, clockwise);
        return true;
    }
}
