using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EditingControllerNew : MonoBehaviour
{
    public EditingUIManager editingUIManager;
    public Placeable[] placeables;
    private int currentTileSelected;
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
        if (!Input.GetKey(KeyCode.D))
        {
            if (nbBlocksAvailable[currentTileSelected] - blockUsage[currentTileSelected] > 0)
            {
                tilePlacer.Edit();
            }
        }
        else
        {
            tileRemover.EditRemove();
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
            Debug.Log("Placeables Length" + placeables.Length);
            Debug.Log("current tile " + currentTileSelected);
            tilePlacer.placeable = placeables[currentTileSelected];
            tileRemover.placeable = placeables[currentTileSelected];

        }
    }

    
}