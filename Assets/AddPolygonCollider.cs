using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Reflection;

//public class AddPolygonCollider : MonoBehaviour
//{
//    private PolygonCollider2D col;
//    private int pointsLength;
//    private int currentPointCount;
//    private int foundColliderCount = 0;
//
//    private Vector2[] emptySpaces;
//    private Vector2[] filledSpaces;
//
//    public void SetPoints()
//    {
//        col = GetComponent<PolygonCollider2D>();
//
//        for (int i = 0; i < transform.childCount; i++)
//        {
//            SurveyAroundChild(transform.GetChild(i));
//
//            if (foundColliderCount == 8)
//            {
//                continue;
//            }
//            else if (foundColliderCount == 7)
//            {
//                pointsLength++;
//            }
//            else
//            {
//                pointsLength += 4;
//            }
//        }
//
//        for (int i = 0; i < transform.childCount; i++)
//        {
//            SurveyAroundChild(transform.GetChild(i));
//            col.points = new Vector2[pointsLength];
//
//            if (foundColliderCount == 8)
//            {
//                continue;
//            }
//            else if (foundColliderCount == 7)
//            {
//                for (int p = 0; p < emptySpaces.Length; p++)
//                {
//                    if (emptySpaces[p] != Vector2.positiveInfinity)
//                    {
//                        col.points[currentPointCount] = emptySpaces[p];
//                        currentPointCount++;
//                    }
//                }
//            }
//            else
//            {
//                for (int a = 0; a < 4; a++)
//                {
//                    
//                }
//            }
//        }
//
//
//    }
//
//    public void SurveyAroundChild(Transform child) 
//    {
//        bool verifiedSurroundings = false;
//        emptySpaces = new Vector2[8];
//        filledSpaces = new Vector2[8]; 
//        for (int i = 1; verifiedSurroundings == true; i *= -1) 
//        {
//            for (int a = 1; verifiedSurroundings == true; a *= -1)
//            {
//                if (Physics2D.OverlapArea((Vector2)child.position + new Vector2(i*0.5f, a*0.5f), (Vector2)child.position + new Vector2(1.5f *i, 1.5f*a)))
//                {
//                    foundColliderCount++;
//                    emptySpaces[i] = Vector2.positiveInfinity;
//                    filledSpaces[i] = (Vector2)child.position + new Vector2(i * 0.5f, a * 0.5f);
//
//                }
//                else
//                {
//                    emptySpaces[i] = (Vector2)child.position + new Vector2(i,a);
//                }
//
//                if (i == -1 && a == -1)
//                {
//                    verifiedSurroundings = true;
//                }
//            }        
//        }
//    }
//}
