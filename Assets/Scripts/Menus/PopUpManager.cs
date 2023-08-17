using System;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public enum PopUpPosition
    {
        World,
        Canvas,
    }
    
    private static Canvas _canvas;
    private static List<GameObject> _instantiatedPopUps;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    public static void CreatePopUp(String text, Vector2 position, PopUpPosition positionType)
    {
        if (positionType == PopUpPosition.Canvas)
        {
            CreatePopUpCanvas(text, position);
        }
        else
        {
            CreatePopUpWorld(text, position);
        }
    }

    static void CreatePopUpCanvas(String text, Vector2 position)
    {
    }

    static void CreatePopUpWorld(String text, Vector2 position)
    {
    }
}
