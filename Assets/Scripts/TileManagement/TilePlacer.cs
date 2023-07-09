using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// This script is used on the player object to place tiles.
/// </summary>
/// Change the _placeable object from another script to control which object is being placed.
/// TODO: manage placeable rotation in here (just call a function on the placeable script)
/// TODO: [EXTRA] the rotation function in the Placeable script can return a boolean and we can play an animation
/// TODO: [EXTRA, CONT'] when the tile placer receives false. (Shake the placeholderPreview tilemap grid?)
public class TilePlacer : MonoBehaviour
{
    public enum Rotation
    {
        Right,
        Bottom, 
        Left, 
        Top
    }
    public Rotation rotation;

    private Tilemap _previewTilemap;
    private Camera _camera;
    private Animator _animator;
    private PlacePreviewTileManager _placePreviewTileManager;

    private int _invalidPlacementAnimatorHash;

    public Placeable placeable; // TODO: add [HideInInspector]

    private Inputs inputManager;
    // Start is called before the first frame update
    void Awake()
    {
        inputManager = new Inputs();
        inputManager.Editing.Enable();
        var previewTilemapObject = transform.Find("PlacePreview");
        _previewTilemap = previewTilemapObject.GetComponent<Tilemap>();
        _animator = previewTilemapObject.GetComponent<Animator>();
        _placePreviewTileManager = previewTilemapObject.GetComponent<PlacePreviewTileManager>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _previewTilemap = transform.Find("PlacePreview").GetComponent<Tilemap>();

        _invalidPlacementAnimatorHash = Animator.StringToHash("InvalidPlacement");
    }

    // Update is called once per frame
    void Update()
    {
        _previewTilemap.ClearAllTiles();
        var position = _previewTilemap.WorldToCell(_camera.ScreenToWorldPoint(Input.mousePosition));

        var gridExtension = placeable.tilePreview.grid.GetExtension();

        for (int i = -(int)gridExtension; i <= gridExtension; i++)
        {
            for (int j = -(int)gridExtension; j <= gridExtension; j++)
            {
                var tileID = placeable.tilePreview.GetTile(i, j, (int)rotation);
                
                if (tileID == -1)
                    continue;
                
                TileBase tile;
                if (tileID < -1)
                {
                    tile = _placePreviewTileManager.GetTile((-tileID) - 2);
                }
                else
                {
                    tile = placeable.tilePreview.tiles[placeable.tilePreview.GetTile(i, j, (int)rotation)];
                }

                var pos = new Vector3Int(position.x + i, position.y + j, position.z);

                _previewTilemap.SetTile(pos, tile);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlaceTile(position);
        }

        if (inputManager.Editing.Rotate.WasPressedThisFrame())
        {
            Rotate((int)inputManager.Editing.Rotate.ReadValue<float>());
        }
    }

    void PlaceTile(Vector3Int position)
    {
        if (!placeable.OnPlace(position))
        {
            _animator.SetTrigger(_invalidPlacementAnimatorHash);
        }
    }

    private void Rotate(int direction)
    {
        if (direction > 0 && rotation == Rotation.Top)
            rotation = Rotation.Right;
        else if (direction < 0 && rotation == Rotation.Right)
            rotation = Rotation.Top;
        else if (direction > 0)
            rotation++;
        else
            rotation--;

    }
}

// Top = (j * -1, i * 1)
// Left = (-i, -j)
// Bottom = (j, -i)
// Right = (i, j)
// 
// sac : 22
