using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//public class LevelBlockManager : MonoBehaviour
//{
//    [HideInInspector]public static Type[] types;
//    [HideInInspector]public LevelBlockBehavior[] behaviors;
//    public ZoneBehavior zoneManager;
//    public LevelBlockBehavior levelBlockBehavior;
//
//
//    private void OnEnable()
//    {
//        zoneManager.onPlaced += levelBlockBehavior.OnPlaced;
//    }
//    public LevelBlockBehavior[] GetBehaviors()
//    {
//        LevelBlockBehavior levelBlockBehavior = GetComponent<LevelBlockBehavior>();
//        types = GetSubclasses.GetInheritedClasses(levelBlockBehavior.GetType());
//        behaviors = new LevelBlockBehavior[types.Length];
//
//        for (int i = 0; i < types.Length; i++)
//        {
//            behaviors[i] = (LevelBlockBehavior)Activator.CreateInstance(types[i]);
//            Debug.Log(types[i].Name);
//        }
//
//        return behaviors;
//    }
//
//}