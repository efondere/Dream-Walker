using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomEditorController : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        Level_Block_Behaviour level_Block_Behaviour = (Level_Block_Behaviour)target;
        if (level_Block_Behaviour.canDisappear)
        {
            Debug.Log("AJO");
            SerializedProperty e_canDisappear = serializedObject.FindProperty("canDisappear");
            EditorGUILayout.FloatField(level_Block_Behaviour.startTimeBeforeDisap, "Time Before Disappear");
        }

        serializedObject.ApplyModifiedProperties();
    }
}
