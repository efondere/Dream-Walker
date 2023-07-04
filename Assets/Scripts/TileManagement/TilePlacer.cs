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
    private Tilemap _previewTilemap;
    private Camera _camera;
    private Animator _animator;

    public Placeable placeable; // TODO: add [HideInInspector]

    // Start is called before the first frame update
    void Start()
    {
        // TODO: does this go in Awake() instead?
        var previewTilemapObject = transform.Find("PlacePreview");
        _previewTilemap = previewTilemapObject.GetComponent<Tilemap>();
        _animator = previewTilemapObject.GetComponent<Animator>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _previewTilemap = transform.Find("PlacePreview").GetComponent<Tilemap>();
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
                // TODO: we should also show -ve tiles
                if (placeable.tilePreview.grid.GetTile(i, j) <= -1)
                    continue;

                var tile = placeable.tilePreview.tiles[placeable.tilePreview.grid.GetTile(i, j)];
                var pos = new Vector3Int(position.x + i, position.y + j, position.z);

                _previewTilemap.SetTile(pos, tile);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlaceTile(position);
        }
    }

    void PlaceTile(Vector3Int position)
    {
        if (!placeable.OnPlace(position))
        {
            //TODO: Is there a better way to trigger the animation?
            _animator.SetTrigger("InvalidPlacement");
        }
    }
}
