using UnityEditor;
using UnityEngine;

// TODO: we should make our own asset type in the future
// we should export to more than just an image, as we'll need other data like the
// preview tiles, etc.
[CustomEditor(typeof(TilemapToGO))]
public class TilemapToGOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TilemapToGO tmToGO = (TilemapToGO)target;

        if (tmToGO.goParent != null) {
            if (GUILayout.Button("Generate Gameobject")) {
                tmToGO.FindBounds();
                tmToGO.CreateGO();
            }
        }
    }
}
