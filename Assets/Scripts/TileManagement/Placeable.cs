using UnityEngine;

[RequireComponent(typeof(TilePreview))]
public class Placeable : MonoBehaviour
{
    [HideInInspector] public TilePreview tilePreview;
    protected TilemapManager _tilemapManager;

    private void Awake()
    {
        tilePreview = GetComponent<TilePreview>();
        _tilemapManager = GameObject.FindWithTag("TilemapManager").GetComponent<TilemapManager>();
    }

    public virtual bool OnPlace(Vector3Int position)
    {
        return false;
    }
}
