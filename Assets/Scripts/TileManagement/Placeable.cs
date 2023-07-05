using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TilePreview))]
public class Placeable : MonoBehaviour
{
    [HideInInspector] public TilePreview tilePreview;
    protected TilemapManager _tilemapManager;

    private void Awake() // TODO: is there a way to make sure this function does not get overriden by accident?
    {
        tilePreview = GetComponent<TilePreview>();
        _tilemapManager = GameObject.FindWithTag("TilemapManager").GetComponent<TilemapManager>();
    }

    public virtual bool OnPlace(Vector3Int position)
    {
        return false;
    }
}
