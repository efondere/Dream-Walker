using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;
using System.IO.Enumeration;

[CustomEditor(typeof(TilemapToTexture))]
public class TilePrefabEditor : Editor
{
    string fileName = ""; 
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();  
        TilemapToTexture tmToTx = (TilemapToTexture)target;
       if (tmToTx.tx == null)
       {
           if (GUILayout.Button("Generate Texture"))
           {
               tmToTx.CreateTx();
           }
       }
       else
       {
           GUILayout.Label("Enter file name");
           fileName = GUILayout.TextField(fileName);
           if (fileName.Length > 0)
           {
               if (GUILayout.Button("Export to PNG"))
               {
                   tmToTx.ExportAsPng(fileName);
               }
           }

            GUILayout.Space(5);
            if (GUILayout.Button("Reset"))
            {
                tmToTx.tx = null;
            }
        }
   


        
    }

}
