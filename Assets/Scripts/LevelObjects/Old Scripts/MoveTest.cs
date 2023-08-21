using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class MoveTest : BlockBehaviorManager
//{
//    private Vector3 velocity = Vector3.zero;
//
//
//
//    private void Start()
//    {
//        StartBehavior(Move);
//    }
//
//    public void Move()
//    {
//        Debug.Log("Moving");
//        if (Mathf.Abs(this.transform.position.x - 10f) <= 0.1 && Mathf.Abs(this.transform.position.y - 10f) <= 0.1)
//        {
//            StopBehavior(Move);
//        }
//        else
//        {
//            Vector3.SmoothDamp(this.transform.position, new Vector2(10, 10), ref velocity, 2f);
//        }
//    }
//}
