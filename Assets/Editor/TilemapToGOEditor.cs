using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TilemapToGO))]
public class TilemapToGOEditor : Editor
{
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        TilemapToGO tmToGO = (TilemapToGO)target;

        if (tmToGO.goParent != null) {
            if (GUILayout.Button("Generate GO"))
            {
                tmToGO.FindBounds();
                tmToGO.CreateGO();
            }
        }
    }
}
