using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
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

    public Placeable placeable; // TODO: add [HideInInspector]

    private Inputs inputManager;
    // Start is called before the first frame update
    void Start()
    {
        
        inputManager = new Inputs();
        inputManager.Editing.Enable();
        
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
                if (placeable.tilePreview.GetTile(i, j, (int)rotation) <= -1)
                    continue;
                var tile = placeable.tilePreview.tiles[placeable.tilePreview.GetTile(i, j, (int)rotation)];
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
            //TODO: Is there a better way to trigger the animation?
            _animator.SetTrigger("InvalidPlacement");
        }
    }

    private void Rotate(int direction)
    {
        if (direction > 0 && rotation == Rotation.Top)
            rotation = Rotation.Right;
        else if(direction < 0 && rotation == Rotation.Right)
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