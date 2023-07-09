using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ResizableTilemap))]
public class ResizableTilemapEditor : PropertyDrawer
{
    private bool _showGrid = true; // this is recommended by unity, however it is reset whenever we enter play mode...
    private int[] _tempData = { -1 }; // this array will be kept at the largest size ever used to prevent losing data when resizing
    // however, it will be reset every time play mode is entered/exited (which is fine)
    
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
            var boxPosition = position;
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
        var newExtension = (int)property.FindPropertyRelative("_extension").uintValue;
        var newSize = 2 * newExtension + 1;
        if (arrayProperty.arraySize != newSize * newSize)
        {
            int tempExtension;
            
            // 1. copy data into _tempData, saving all changes made to the array.
            if (_tempData.Length <= arrayProperty.arraySize)
            {
                // old data was smaller or of equal size, just resize it and override its data.
                _tempData = new int[arrayProperty.arraySize];
                for (var i = 0; i < _tempData.Length; i++)
                {
                    _tempData[i] = arrayProperty.GetArrayElementAtIndex(i).intValue;
                }

                tempExtension = (Mathf.RoundToInt(Mathf.Sqrt(_tempData.Length)) - 1) / 2;
            }
            else
            {
                // old data was of bigger size. Don't change the size, but update the contents of the overlapping region
                var oldSize = Mathf.RoundToInt(Mathf.Sqrt(arrayProperty.arraySize));
                var oldExtension = (oldSize - 1) / 2;

                var tempSize = Mathf.RoundToInt(Mathf.Sqrt(_tempData.Length));
                tempExtension = (tempSize - 1) / 2;
                
                for (var y = -oldExtension; y <= oldExtension; y++)
                {
                    for (var x = -oldExtension; x <= oldExtension; x++)
                    {
                        _tempData[(x + tempExtension) + tempSize * (y + tempExtension)] =
                            arrayProperty.GetArrayElementAtIndex((x + oldExtension) + oldSize * (y + oldExtension))
                                .intValue;
                    }
                }
            }

            arrayProperty.arraySize = (int)(newSize * newSize);

            for (var y = -newExtension; y <= newExtension; y++)
            {
                for (var x = -newExtension; x <= newExtension; x++)
                {
                    if (Mathf.Abs(y) > tempExtension || Mathf.Abs(x) > tempExtension)
                    {
                        arrayProperty.GetArrayElementAtIndex(
                            ((x + newExtension) + newSize * (y + newExtension))).intValue = -1;
                    }
                    else
                    {
                        var tempSize = (tempExtension * 2) + 1;
                        arrayProperty.GetArrayElementAtIndex(
                                ((x + newExtension) + newSize * (y + newExtension))).intValue =
                            _tempData[x + tempExtension + tempSize * (y + tempExtension)];
                    }
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