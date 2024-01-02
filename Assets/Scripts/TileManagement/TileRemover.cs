using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRemover : MonoBehaviour
{
    private Tilemap removerTm;
    public TileBase removeTile;
    private Tilemap solidTm;
    public delegate void Remove(Vector2Int pos, TilePreview tilePreview);
    public static event Remove OnRemove;
    [HideInInspector] public Placeable placeable;

    //public class Block
    //{
    //    public TilePreview tilePreview;
    //    public Vector2Int pos;
    //    public List<Vector2Int> positions = new();
    //}
    //
    //List<Block> blocks = new List<Block>();

    private void Awake()
    {
        removerTm = transform.Find("RemovePreview").GetComponent<Tilemap>();
        solidTm = transform.Find("Solid").GetComponent<Tilemap>();
        //PlaceableTile.onPlaceEvent += SaveChanges;
    }



    public void EditRemove()
    {
        removerTm.ClearAllTiles();
        var position = removerTm.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        var gridExtension = placeable.tilePreview.grid.GetExtension();

        for (int i = -(int)gridExtension; i <= gridExtension; i++)
        {
            for (int j = -(int)gridExtension; j <= gridExtension; j++)
            {
                var tileID = placeable.tilePreview.GetTile(i, j);
                if (tileID >= 0)
                {
                    removerTm.SetTile(position + new Vector3Int(i, j, 0), removeTile);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RemoveTile(position);
        }

    }

    void RemoveTile(Vector3Int position)
    {
        var gridExtension = placeable.tilePreview.grid.GetExtension();
        for (int i = -(int)gridExtension; i <= gridExtension; i++)
        {
            for (int j = -(int)gridExtension; j <= gridExtension; j++)
            {
                var tileID = placeable.tilePreview.GetTile(i, j);
                if (tileID >= 0)
                {
                    solidTm.SetTile(position + new Vector3Int(i, j, 0), null);
                }
            }
        }
        OnRemove?.Invoke((Vector2Int)position, placeable.tilePreview);
    }

    ////    void SaveChanges(Vector2Int pos, TilePreview tilePreview)
    ///
    //  {
    //      blocks.Add(new());
    //      blocks[blocks.Count - 1].tilePreview = tilePreview;
    //      blocks[blocks.Count -1 ].pos = pos;
    //
    //      var gridExtension = (int)blocks[blocks.Count - 1].tilePreview.grid.GetExtension();
    //      for (int i = -gridExtension; i <= gridExtension; i++)
    //      {
    //          for (int j = -gridExtension; j <= gridExtension; j++)
    //          {
    //              if (tilePreview.GetTile(i,j) >= 0)
    //              {
    //                  blocks[blocks.Count - 1].positions.Add(pos + new Vector2Int(i, j));
    //              }
    //          }
    //      }
    //  }
    ////
    ////    public void EditRemove()
    //  {
    //      removerTm.ClearAllTiles();
    //      var position = (Vector2Int)removerTm.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    //      Block selectedBlock = new Block();
    //      foreach (var block in blocks)
    //      {
    //          if (block.positions.Contains(position))
    //          {
    //              selectedBlock = block;
    //              break;
    //          }
    //      }
    //
    //      foreach (var pos in selectedBlock.positions)
    //      {
    //          removerTm.SetTile((Vector3Int)pos, removeTile);
    //      }
    //
    //      if (Input.GetMouseButtonDown(0))
    //      {
    //          RemoveTile(selectedBlock);
    //      }
    //
    //  }
    ////
    ////
    ////    void RemoveTile(Block block)
    //   { 
    //     foreach (var pos in block.)
    //     {
    //         solidTm.SetTile((Vector3Int)pos, null);
    //     }
    // 
    //     blocks.Remove(block);
    //     OnRemove?.Invoke(block.centerPos, block);
    //    }



}
