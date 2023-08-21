using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class EditingUIManager : MonoBehaviour
{
    private Inputs _inputs;
    public GameObject[] tilesUI;
    public GameObject tileSelectorUI;


    public void SetSelectorUI(int[] nbBlocksAvailable)
    {
        for (int i = 0; i < tilesUI.Length; i++)
        {
            tilesUI[i].GetComponentInChildren<Text>().text = "x" + nbBlocksAvailable[i];
        }
    }

    public void useBlock(int ID, int display)
    {
       tilesUI[ID].GetComponentInChildren<Text>().text = "x" + (display);
    }


    public void UpdateSelector(int m_currentlySelectedTile)
    {
        tileSelectorUI.GetComponent<RectTransform>().position = tilesUI[m_currentlySelectedTile].GetComponent<RectTransform>().position;
    }

}
