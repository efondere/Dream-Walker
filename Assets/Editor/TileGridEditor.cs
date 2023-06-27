using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening.Plugins;

[CustomPropertyDrawer(typeof(TileGrid))]
public class TileGridEditor : PropertyDrawer
{
    // allow to extend edges, not the full size of the grid (ie. extend by 0 is 1x1, 1 is 3x3, 2 is 5x5, etc.
    // use Resizable grid to create a custom property drawer
    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    EditorGUI.PrefixLabel(position, label);
    //
    //    var data = property.FindPropertyRelative("_data.length");
    //    Debug.Log(data);
    //
    //    //var enumerator = property.GetEnumerator();
    //    //while (enumerator.MoveNext())
    //    //{
    //    //    //Debug.Log((enumerator.Current as SerializedProperty).type);
    //    //
    //    //}
    //
    //    //EditorGUILayout.PropertyField(data.FindPropertyRelative("size"));
    //
    //    //Debug.Log(property.FindPropertyRelative("_data").arraySize);
    //}

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);
    
        Rect newPosition = position;
        newPosition.y += 18f;
        SerializedProperty rows = property.FindPropertyRelative("rows");
    
        for (int i = 0; i < 5; i++)
        {
            SerializedProperty row = rows.GetArrayElementAtIndex(i).FindPropertyRelative("row");
            newPosition.height = 20;
    
            if (row.arraySize != 5)
                row.arraySize = 5;
    
            newPosition.width = 70;
    
            for (int j = 0; j < 5; j++)
            {
                EditorGUI.PropertyField(newPosition, row.GetArrayElementAtIndex(j), GUIContent.none);
                newPosition.x += newPosition.width;
            }
    
            newPosition.x = position.x;
            newPosition.y += 20;
        }
    }
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 20 * 12;
    }
}
