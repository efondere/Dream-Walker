using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacer : MonoBehaviour
{
    [SerializeField] private Tilemap _previewTilemap; // give ref to grid here instead
    [SerializeField] private Tilemap _physicalTilemap;
    [SerializeField] private Camera _camera;
    [SerializeField] private Animator _animator; // get compoenent instead

    [SerializeField] private Placeable _placeable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _previewTilemap.ClearAllTiles();
        var position = _previewTilemap.WorldToCell(_camera.ScreenToWorldPoint(Input.mousePosition));

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (_placeable.tilePreview.grid.At(i, j) == -1)
                    continue;

                var tile = _placeable.tilePreview.tiles[_placeable.tilePreview.grid.At(i, j)];
                var pos = new Vector3Int(position.x - 2 + i, position.y - 2 + j, position.z);

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
        if (!_placeable.OnPlace(position, _previewTilemap as GridLayout))
        {
            _animator.SetTrigger("InvalidPlacement");
        }
    }
}
