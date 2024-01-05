using UnityEngine;

[RequireComponent(typeof(TilePlacer))]
[RequireComponent(typeof(TileRemover))]
public class EditingController : MonoBehaviour
{
    private bool _isEditing;
    private bool _isErasing;

    public EditingUIManager editingUIManager;
    public Placeable[] placeables;
    private int _selectedTileIndex;
    Inputs _inputs;
    public int[] nbBlocksAvailable;
    private int[] blockUsage;
    TilePlacer tilePlacer;
    TileRemover tileRemover;

    private void OnEnable()
    {
        _inputs = new Inputs();
        _inputs.Enable();
        tilePlacer = GetComponent<TilePlacer>();
        tileRemover = GetComponent<TileRemover>();
    }

    private void Start()
    {
        _selectedTileIndex = nbBlocksAvailable.Length / 2;
        editingUIManager.UpdateSelector(_selectedTileIndex);
        editingUIManager.SetSelectorUI(nbBlocksAvailable);
        tilePlacer.placeable = placeables[_selectedTileIndex];
        tileRemover.placeable = placeables[_selectedTileIndex];
        blockUsage = new int[nbBlocksAvailable.Length];
    }


    public void useBlock()
    {
        blockUsage[_selectedTileIndex]++;
        editingUIManager.useBlock(_selectedTileIndex, nbBlocksAvailable[_selectedTileIndex] - blockUsage[_selectedTileIndex]);
    }

    private void Update()
    {
        if (!Input.GetKey(KeyCode.D))
        {
            if (nbBlocksAvailable[_selectedTileIndex] - blockUsage[_selectedTileIndex] > 0)
            {
                tilePlacer.Edit();
            }
        }
        else
        {
            tileRemover.EditRemove();
        }


    }

    public bool IsEditing()
    {
        return _isEditing;
    }

    public void BeginEditing()
    {
        _isEditing = true;
        _isErasing = false;
    }

    public void StopEditing()
    {
        _isErasing = false;
        _isEditing = false;
    }

    public void BeginErasing()
    {
        _isErasing = true;
    }

    public void StopErasing()
    {
        _isErasing = false;
    }

    public void SelectNext()
    {
        _selectedTileIndex++;

        if (_selectedTileIndex >= nbBlocksAvailable.Length)
            _selectedTileIndex = 0;

        editingUIManager.UpdateSelector(_selectedTileIndex);
        tilePlacer.placeable = placeables[_selectedTileIndex];
        tileRemover.placeable = placeables[_selectedTileIndex];
    }

    public void SelectPrev()
    {
        _selectedTileIndex--;

        if (_selectedTileIndex < 0)
        {
            _selectedTileIndex = nbBlocksAvailable.Length - 1;
        }

        editingUIManager.UpdateSelector(_selectedTileIndex);
        tilePlacer.placeable = placeables[_selectedTileIndex];
        tileRemover.placeable = placeables[_selectedTileIndex];
    }
}
