using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableTile : Placeable
{
    [SerializeField] public Tilemap tileset;

    public override bool OnPlace(Vector3Int position, GridLayout grid)
    {
        var gridExtension = tilePreview.grid.GetExtension();

        for (int i = -(int)gridExtension; i < gridExtension; i++)
        {
            for (int j = -(int)gridExtension; j < gridExtension; j++)
            {
                if (tilePreview.grid.GetTile(i, j) == -1) // -1 is no tile, 0 is air visualization
                    continue;

                var pos = new Vector3Int(position.x + i, position.y + j, position.z);
                if (collisionManager.isColliding(pos))
                {
                    return false;
                }
            }
        }

        for (int i = -(int)gridExtension; i < gridExtension; i++)
        {
            for (int j = -(int)gridExtension; j < gridExtension; j++)
            {
                if (tilePreview.grid.GetTile(i, j) == -1)
                    continue;
                if (tilePreview.grid.GetTile(i, j) == 0)
                    continue;

                var tile = tilePreview.tiles[tilePreview.grid.GetTile(i, j)];
                var pos = new Vector3Int(position.x + i, position.y + j, position.z);

                tileset.SetTile(pos, tile);
            }
        }

        return true;
    }
}
