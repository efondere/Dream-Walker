using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockBehaviorManager : MonoBehaviour
{
    public delegate void OnUpdate();
    public static OnUpdate onUpdate;
    public UnityEvent onStartBehaviors;

    private static BlockBehaviorManager blockBehaviorManager;
    
    public static BlockBehaviorManager GetInstance()
    {
        if (blockBehaviorManager == null)
        {
            blockBehaviorManager = new BlockBehaviorManager();
        }
        return blockBehaviorManager;
    }

    private void Start()
    {
        onStartBehaviors.Invoke();
    }

    public static void StartBehavior(OnUpdate methodToStart)
    {
        onUpdate += methodToStart;
    }

    public static void StopBehavior(OnUpdate methodToStop)
    {
        onUpdate -= methodToStop;
    }  
}
