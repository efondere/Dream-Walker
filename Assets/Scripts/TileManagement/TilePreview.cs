using DG.Tweening.Plugins;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePreview : MonoBehaviour
{
    [SerializeField] public TileBase[] tiles;
    [SerializeField] public ResizableTilemap grid;
    
    // grid indices:
    // -1 : no tile (air)
    // <= -2: placeholder / clearance tile. We will support values other than just -2 for various types of placeholder tiles (ie. colors?)
    // >= 0: various tiles from the tiles array.
}
