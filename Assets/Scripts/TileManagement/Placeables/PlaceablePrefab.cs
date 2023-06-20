using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceablePrefab : Placeable
{
    [SerializeField] public Tilemap tileset;
    [SerializeField] public Tilemap placeholderPreview;
    [SerializeField] public GameObject prefab;
    [SerializeField] public AnimatedTile placeholder_tile;

    public override bool OnPlace(Vector3Int position, GridLayout grid)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (tilePreview.grid.At(i, j) == -1) // -1 is no tile, 0 is air visualization
                    continue;

                var pos = new Vector3Int(position.x - 2 + i, position.y - 2 + j, position.z);
                if (collisionManager.isColliding(pos))
                {
                    return false;
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (tilePreview.grid.At(i, j) < 0)
                    continue;

                var pos = new Vector3Int(position.x - 2 + i, position.y - 2 + j, position.z);
                
                if (tilePreview.grid.At(i, j) == 0)
                {
                    placeholderPreview.SetTile(pos, placeholder_tile);
                }
                else
                {
                    var tile = tilePreview.tiles[tilePreview.grid.At(i, j)];
                    placeholderPreview.SetTile(pos, tile);
                }
            }
        }

        Instantiate(prefab, grid.CellToWorld(position), Quaternion.identity);

        return true;
    }
}
