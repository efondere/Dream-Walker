using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ResizableTilemap))]
public class ResizableTilemapEditor : PropertyDrawer
{
    private bool _showGrid = true; // this is recommended by unity, however it is reset whenever we enter play mode...
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position.height = EditorGUIUtility.singleLineHeight;

            
        _showGrid = EditorGUI.BeginFoldoutHeaderGroup(position, _showGrid, label);
        
        var arrayProperty = property.FindPropertyRelative("_tiles");
        
        if (_showGrid)
        {
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_extension"));
            if (property.FindPropertyRelative("_extension").uintValue > 10)
            {
                // seems like there is not need to check for this value being < 0 as it's a uint.
                property.FindPropertyRelative("_extension").uintValue = 10; // prevent crash!
            }

            position.y += EditorGUIUtility.singleLineHeight;
            var sideLength = Mathf.RoundToInt(Mathf.Sqrt(arrayProperty.arraySize));
            var boxPosition = position; //TODO: check indent! (and fix here in `position` variable if needed)
            boxPosition.width /= sideLength;
            
            for (int y = 0; y < sideLength; y++)
            {
                for (int x = 0; x < sideLength; x++)
                {
                    // Unity defines +y as up and we'll store the data in a similar way (hence the sideLength - 1 - y).
                    EditorGUI.PropertyField(boxPosition, arrayProperty.GetArrayElementAtIndex(
                        x + sideLength * (sideLength - 1 - y)), GUIContent.none); // GUIContent.none => don't show name label
                    boxPosition.x += boxPosition.width;
                }

                boxPosition.x = position.x;
                boxPosition.y += EditorGUIUtility.singleLineHeight;
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndFoldoutHeaderGroup();
        
        //update array after so property height is changed before re-drawing the grid (hopefully) -> check if this is actually ok
        // TODO: to prevent losing data when modifying the value in the extension, we should check that "_extension" != 0
        // TODO (Cont'): before getting rid of the old data. + DOES THIS APPLY TO ANY DOWNSIZING?
        var extension = property.FindPropertyRelative("_extension").uintValue;
        var newSize = 2 * extension + 1;
        if (arrayProperty.arraySize != newSize * newSize)
        {
            var oldSize = Mathf.RoundToInt(Mathf.Sqrt(arrayProperty.arraySize));
            var oldExtension = (oldSize - 1) / 2;
            
            int[] oldData = new int[arrayProperty.arraySize];
            for (int i = 0; i < arrayProperty.arraySize; i++)
            {
                oldData[i] = arrayProperty.GetArrayElementAtIndex(i).intValue; // copy old data;
            }

            arrayProperty.arraySize = (int)(newSize * newSize);

            for (var y = -extension; y < extension; y++)
            {
                for (var x = -extension; x < extension; x++)
                {
                    if (Mathf.Abs(y) > oldExtension || Mathf.Abs(x) > oldExtension)
                        arrayProperty.GetArrayElementAtIndex(
                            (int)((x + extension) + newSize * (y + extension))).intValue = -1;
                    else
                        arrayProperty.GetArrayElementAtIndex(
                            (int)((x + extension) + newSize * (y + extension))).intValue =
                            oldData[x + oldExtension + oldSize * (y + oldExtension)];
                }
            }
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (_showGrid)
        {
            // 3: property name, _extension property editor, and center row
            return EditorGUIUtility.singleLineHeight * (3 + 2 * property.FindPropertyRelative("_extension").uintValue);
        }
        else
        {
            // only show property name (foldout group)
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
