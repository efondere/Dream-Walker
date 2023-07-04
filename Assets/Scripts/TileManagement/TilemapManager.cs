using UnityEngine;
using UnityEngine.Tilemaps;

//TODO: change this to `TilemapManager` and handle placing tiles in the different tilemaps from here.
//We can then pass it to the `Placeable`s.
public class TilemapManager : MonoBehaviour
{
    private Tilemap _solidTilemap;
    private PlaceholderPreviewer _placeholderTilemap;
    
    void Start() //TODO: or Awake() ?
    {
        // TODO: is this necessary since this is going to be a prefab?
        var solidObject = transform.Find("Solid");
        var placeholderObject = transform.Find("Placeholder");
        placeholderObject.position = solidObject.position; // align both tilesets to be sure!
        
        _solidTilemap = solidObject.GetComponent<Tilemap>();
        _placeholderTilemap = placeholderObject.GetComponent<PlaceholderPreviewer>();
    }
    
    public bool IsColliding(Vector3Int pos)
    {
        if (_solidTilemap.GetTile(pos) != null)
        {
            return true;
        } 
        if (_placeholderTilemap.GetTile(pos.x, pos.y) != -1)
        {
            return true;
        }

        return false;
    }

    public void PlaceSolidTile(Vector3Int pos, TileBase tile)
    {
        _solidTilemap.SetTile(pos, tile);
    }

    public void PlacePlaceholderTile(Vector3Int pos, int tileID)
    {
        _placeholderTilemap.SetTile(pos.x, pos.y, -tileID);
    }

    public Vector3 CellToWorld(Vector3Int pos)
    {
        return _solidTilemap.CellToWorld(pos);
    }
}
