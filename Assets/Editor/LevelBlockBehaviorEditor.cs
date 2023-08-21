using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

[CustomEditor(typeof(LevelBlockBehavior)), CanEditMultipleObjects]
public class LevelBlockBehaviorEditor : Editor
{
    bool afterEvent;

    List<string> strings = new List<string>();
    public int m_numberOfBehaviors;

    float myFloat;

    public override void OnInspectorGUI()
     {

        base.OnInspectorGUI();

        EditorGUILayout.Space(10);

        
        LevelBlockBehavior levelBlockBehavior = (LevelBlockBehavior)target;


        GUIStyle intStyle = GUI.skin.box;
        int savedNumberOfBehaviors = m_numberOfBehaviors;
        m_numberOfBehaviors = EditorGUILayout.IntField("number of behaviors", m_numberOfBehaviors, intStyle);
        if( m_numberOfBehaviors <= 0)
        {
            m_numberOfBehaviors = savedNumberOfBehaviors;
        }
        else if (m_numberOfBehaviors >= 10)
        {
            m_numberOfBehaviors = 10;
        }

        EditorGUILayout.Space(10);


        for (int i = 0; i < m_numberOfBehaviors; i++)
        {
            //EditorGUI.indentLevel++;

             
            int selectedBehaviorIndex = EditorGUILayout.Popup("Select Behaviors",0, GetBehaviorNames(new LevelBlockBehavior()).ToArray());
            Type selectedBehaviorType = GetBehaviorTypes(levelBlockBehavior)[selectedBehaviorIndex];

            //object selectedBehavior = selectedBehaviorType.Instantiate();
            //UnityEngine.Object behavior = EditorGUILayout.ObjectField(levelBlockBehavior, selectedBehaviorType, true);
            var position = EditorGUILayout.GetControlRect();
            position.x -= position.width / 2;
            for (int a = 0; a < 2; a++) 
            {
                foreach (FieldInfo field in selectedBehaviorType.GetFields().Where(x => x.DeclaringType != typeof(LevelBlockBehavior)))
                {

                    //    Editor.CreateEditor((UnityEngine.Object)selectedBehavior);
                    //    SerializedObject serializedObject = (SerializedObject)field.GetValue(selectedBehavior);


                    SerializedProperty property = serializedObject.FindProperty(field.Name);

                    Debug.Log(property == null);
                    //SerializedProperty serializedProperty = serializedObject.FindProperty(field.Name);


                //    EditorGUILayout.PropertyField(property);


                    //EditorGUILayout.ObjectField(levelBlockManager.levelBlockBehavior, fieldType, false);

                    //SerializedProperty property = serializedObject.                    
                    //EditorGUI.Being(position, property);
                    //position.y -= EditorGUIUtility.singleLineHeight;
                }
                position.x += position.width / 2;

                serializedObject.ApplyModifiedProperties();
            }




            EditorGUILayout.Space(10);
            
        }

        

    
        EditorGUILayout.Space(10);
        //EditorGUILayout.BeginFoldoutHeaderGroup(levelBlockManager.affectedByZone, "Affected by Zone");
        
    
    }

    public List<string> GetBehaviorNames(LevelBlockBehavior levelBlockBehavior)
    {
        var types = GetSubclasses.GetInheritedClasses(levelBlockBehavior.GetType());

        for (int i = 0; i < types.Length; i++)
        {
            strings.Add(types[i].Name);
        }

        return strings;
    }

    public Type[] GetBehaviorTypes(LevelBlockBehavior levelBlockBehavior)
    {
        var types = GetSubclasses.GetInheritedClasses(levelBlockBehavior.GetType());

        return types;
    }







}
