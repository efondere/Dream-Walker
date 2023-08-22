using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class TilePlacer : MonoBehaviour
{
    public EditingControllerNew editingController;

    private Transform _previewObjectTransform;
    private Tilemap _previewTilemap;
    private Camera _camera;
    private Animator _animator;
    private PlacePreviewTileManager _placePreviewTileManager;

    private int _invalidPlacementAnimatorHash;



    // added HideInInspector
    [HideInInspector]public Placeable placeable; // TODO: add [HideInInspector]

    private Inputs inputManager;
    
    // Start is called before the first frame update
    void Awake()
    {
        inputManager = new Inputs();
        inputManager.Editing.Enable();
        
        _previewObjectTransform = transform.Find("PlacePreview");
        _previewTilemap = _previewObjectTransform.GetComponent<Tilemap>();
        _animator = _previewObjectTransform.GetComponent<Animator>();
        _placePreviewTileManager = _previewObjectTransform.GetComponent<PlacePreviewTileManager>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _previewTilemap = transform.Find("PlacePreview").GetComponent<Tilemap>();

        _invalidPlacementAnimatorHash = Animator.StringToHash("InvalidPlacement");
    }

    // Update is called once per frame
    public void Edit()
    {
        _previewTilemap.ClearAllTiles();
        var position = _previewTilemap.WorldToCell(_camera.ScreenToWorldPoint(Input.mousePosition));

        var gridExtension = placeable.tilePreview.grid.GetExtension();

        for (int i = -(int)gridExtension; i <= gridExtension; i++)
        {
            for (int j = -(int)gridExtension; j <= gridExtension; j++)
            {
                var tileID = placeable.tilePreview.GetTile(i, j);
                
                if (tileID == -1)
                    continue;
                
                TileBase tile;
                if (tileID < -1)
                {
                    tile = _placePreviewTileManager.GetTile((-tileID) - 2);
                }
                else
                {
                    tile = placeable.tilePreview.tiles[placeable.tilePreview.GetTile(i, j)];
                }

                var pos = new Vector3Int(position.x + i, position.y + j, position.z);

                _previewTilemap.SetTile(pos, tile);
            }
        }

        // TODO: move this to player input manager
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTile(position);
        }

        if (inputManager.Editing.Rotate.WasPressedThisFrame())
        {
            Rotate(inputManager.Editing.Rotate.ReadValue<float>() > 0);
        }
    }

    public void PlaceTile(Vector3Int position)
    {
        // add rotation for testing
        if (!placeable.OnPlace(position))
        {

            _animator.SetTrigger(_invalidPlacementAnimatorHash);
        }
        else
        {
            editingController.useBlock();
        }
    }

    public void Rotate(bool clockwise)
    {
        if (!placeable.Rotate(clockwise))
        {
            _previewObjectTransform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        }
    }
}

