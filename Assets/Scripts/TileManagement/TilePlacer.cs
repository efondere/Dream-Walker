using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;

/// <summary>
    /// This script is used on the player object to place tiles.
    /// </summary>
    /// Change the _placeable object from another script to control which object is being placed.
    /// TODO: manage placeable rotation in here (just call a function on the placeable script)
    /// TODO: [EXTRA] the rotation function in the Placeable script can return a boolean and we can play an animation
    /// TODO: [EXTRA, CONT'] when the tile placer receives false. (Shake the placeholderPreview tilemap grid?)
public class TilePlacer : MonoBehaviour
{
    [SerializeField] private Tilemap _previewTilemap;
    private Camera _camera;
    private Animator _animator;

    public Placeable placeable; // TODO: add [HideInInspector]

    // Start is called before the first frame update
    void Start()
    {
        // TODO: does this go in Awake() instead?
        _animator = _previewTilemap.GetComponent<Animator>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _previewTilemap.ClearAllTiles();
        var position = _previewTilemap.WorldToCell(_camera.ScreenToWorldPoint(Input.mousePosition));

        var gridExtension = placeable.tilePreview.grid.GetExtension();

        for (int i = -(int)gridExtension; i < gridExtension; i++)
        {
            for (int j = -(int)gridExtension; j < gridExtension; j++)
            {
                // TODO: something is wrong in the way we index this array.
                if (placeable.tilePreview.grid.GetTile(i, j) == -1)
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
        if (!placeable.OnPlace(position, _previewTilemap as GridLayout))
        {
            _animator.SetTrigger("InvalidPlacement");
        }
    }
}
