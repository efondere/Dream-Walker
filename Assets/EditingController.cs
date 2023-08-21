using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EditingController : MonoBehaviour
{
    public EditingUIManager editingUIManager;
    public Placeable[] placeables;
    private int currentTileSelected;
    Inputs _inputs;
    public int[] nbBlocksAvailable;
    private int[] blockUsage;
    TilePlacer tilePlacer;
    bool canEdit = true;

    private void OnEnable()
    {
        _inputs = new Inputs();
        _inputs.Enable();
        tilePlacer = GetComponent<TilePlacer>();
    }

    private void Start()
    {
        currentTileSelected = nbBlocksAvailable.Length/2;
        editingUIManager.UpdateSelector(currentTileSelected);
        editingUIManager.SetSelectorUI(nbBlocksAvailable);
        tilePlacer.placeable = placeables[currentTileSelected];
        blockUsage = new int[nbBlocksAvailable.Length];
    }


    public void useBlock()
    {
        blockUsage[currentTileSelected]++;
        editingUIManager.useBlock(currentTileSelected, nbBlocksAvailable[currentTileSelected] - blockUsage[currentTileSelected]);
    }

    private void Update()
    {
        UpdateSelection();
        if (nbBlocksAvailable[currentTileSelected] - blockUsage[currentTileSelected] > 0)
        {
            tilePlacer.Edit();
        }


    }

    void UpdateSelection()
    {
        if (_inputs.Editing.Select.WasPressedThisFrame())
        {
            if (_inputs.Editing.Select.ReadValue<float>() < 0f)
            {
                currentTileSelected--;
            }
            if (_inputs.Editing.Select.ReadValue<float>() > 0f)
            {
                currentTileSelected++;
            }

            if (currentTileSelected >= nbBlocksAvailable.Length)
            {
                currentTileSelected = 0;
            }
            else if (currentTileSelected < 0)
            {
                currentTileSelected = nbBlocksAvailable.Length - 1;
            }

            editingUIManager.UpdateSelector(currentTileSelected);
            tilePlacer.placeable = placeables[currentTileSelected];
        }
    }

    
}