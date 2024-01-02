using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUpdate : MonoBehaviour
{
    BlockBehaviorManager blockBehaviorManager;
   //private void OnEnable()
   //{
   //    blockBehaviorManager = BlockBehaviorManager.GetInstance();
   //    Debug.Log(blockBehaviorManager.GetInstanceID());
   //}
    private void Update()
    {
        if (BlockBehaviorManager.onUpdate != null)
        BlockBehaviorManager.onUpdate();

        Debug.Log("Update");
    }
}


