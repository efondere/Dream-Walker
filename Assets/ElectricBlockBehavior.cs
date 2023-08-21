using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class ElectricBlockBehavior : MonoBehaviour
{
//    //public PointLight glowActive;
//    //public float glowRange;
//    //private Animator electricFlowAnimator;
//    //int _signalState = 0;
//    [HideInInspector] public bool isConnectedToLever;
//    private Tilemap solidTm;
//    public TileBase electricTile;
//
//    [HideInInspector] public List<ElectricLeverBhvr> connectedLevers;
//    private List<ElectricBlockBehavior> connectedBlocks;
//    private int nbLeversInScene;
//    IEnumerable<ElectricBlockBehavior> electricBlockBehaviors;
//
//
//    private void Awake()
//    {
//        connectedLevers = new List<ElectricLeverBhvr>();
//        connectedBlocks = new List<ElectricBlockBehavior>();
//        nbLeversInScene = GameObject.FindGameObjectsWithTag("Lever").Length;
//        PlaceablePrefab.onPlaceEvent += OnPlace;
//        //electricFlowAnimator = GetComponent<Animator>();
//        //electricFlowAnimator.enabled = false;
//        //_signalState = -1;
//    }
//
//    //    private void OnRemove(GameObject tile)
//    //    {
//    //        if (tile == gameObject)
//    //        {
//    //            RequestConnectionCheck();
//    //        }
//    //    }
//
//    public void ReceiveSignal(int leverState)
//    {
//        Debug.Log("Signal Received");
//        foreach (var animator in GetComponentsInChildren<Animator>())
//        {
//            animator.enabled = (leverState == 0) ? false : true;
//        }
//    }
//
//    void OnPlace(GameObject tile)
//    {
//        //Debug.Log("Number of Levers In Scene"+nbLeversInScene);
//        if (tile.GetComponentInChildren<ElectricBlockBehavior>() && connectedLevers.Count != nbLeversInScene)
//        {
//            //Debug.Log("Check Connections");
//            //CheckConnections();
//        }
//    }
//
//    public void RequestConnectionCheck()
//    {
//        Vector2 pos = (Vector2)transform.position;
//
//        Collider2D rightColl = Physics2D.OverlapPoint(pos + Vector2.right);
//        Collider2D leftColl = Physics2D.OverlapPoint(pos + Vector2.left);
//        Collider2D downColl = Physics2D.OverlapPoint(pos + Vector2.down);
//        Collider2D upColl = Physics2D.OverlapPoint(pos + Vector2.up);
//
//        if (downColl)
//        {
//            if (downColl.GetComponent<ElectricBlockBehavior>())
//            {
//                downColl.GetComponent<ElectricBlockBehavior>().CheckConnections();
//                downColl.GetComponent<ElectricBlockBehavior>().RequestConnectionCheck();
//
//            }
//        }
//
//        if (upColl)
//        {
//            if (upColl.GetComponent<ElectricBlockBehavior>())
//            {
//                if (upColl.GetComponent<ElectricBlockBehavior>() != downColl.GetComponent<ElectricBlockBehavior>())
//                {
//                    upColl.GetComponent<ElectricBlockBehavior>().CheckConnections();
//                    upColl.GetComponent<ElectricBlockBehavior>().RequestConnectionCheck();
//                }
//            }
//        }
//
//        if (rightColl)
//        {
//            if (rightColl.GetComponent<ElectricBlockBehavior>())
//            {
//                if (rightColl.GetComponent<ElectricBlockBehavior>() != downColl.GetComponent<ElectricBlockBehavior>() && rightColl.GetComponent<ElectricBlockBehavior>() != upColl.GetComponent<ElectricBlockBehavior>())
//                {
//                    rightColl.GetComponent<ElectricBlockBehavior>().CheckConnections();
//                    rightColl.GetComponent<ElectricBlockBehavior>().RequestConnectionCheck();
//
//                }
//            }
//        }
//
//        if (leftColl)
//        {
//            if (leftColl.GetComponent<ElectricBlockBehavior>())
//            {
//                if (leftColl.GetComponent<ElectricBlockBehavior>() != downColl.GetComponent<ElectricBlockBehavior>() && leftColl.GetComponent<ElectricBlockBehavior>() != upColl.GetComponent<ElectricBlockBehavior>() && leftColl.GetComponent<ElectricBlockBehavior>() != rightColl.GetComponent<ElectricBlockBehavior>())
//                {
//                    leftColl.GetComponent<ElectricBlockBehavior>().CheckConnections();
//                    leftColl.GetComponent<ElectricBlockBehavior>().RequestConnectionCheck();
//                }
//            }
//        }
//
//    }
//
//    public void ResetConnections()
//    {
//        foreach (var lever in connectedLevers)
//        {
//            connectedLevers.Remove(lever);
//        }
//        foreach (var block in connectedBlocks)
//        {
//            connectedBlocks.Remove(block);
//        }
//
//    }
//
//    private void OnDrawGizmos()
//    {
//        int i = 1;
//        foreach (Transform transform in GetComponentsInChildren<Transform>())
//        {
//
//            if (transform.name != "Rotation Center" && transform != this.transform)
//            {
//                Gizmos.DrawCube((Vector3)((Vector2)transform.position + Vector2.left), i * Vector3.one * 0.1f);
//                i++;
//            }
//        }
//        i = 1;
//    }

//   public CheckConnections()
//   {
//       bool requestConnectionCheck = false;
//       //Debug.Log("GetComponents In Children " + GetComponentsInChildren<Transform>().Length);
//       foreach (Transform transform in transform.GetComponentsInChildren<Transform>())
//       {
//           if (transform.name == "Rotation Center" || transform == this.gameObject.transform)
//           {
//               Debug.Log("Continue");
//               continue;
//           }
//           Vector2 pos = (Vector2)transform.position;
//           Collider2D rightColl = Physics2D.OverlapPoint(pos + Vector2.right);
//           Collider2D leftColl = Physics2D.OverlapPoint(pos + Vector2.left);
//           Collider2D downColl = Physics2D.OverlapPoint(pos + Vector2.down);
//           Collider2D upColl = Physics2D.OverlapPoint(pos + Vector2.up);
//
//           //Debug.Log("Block pos : "+pos + " " + transform.name);
//           //Debug.Log(pos + 2*Vector2.left);
//           //Debug.Log(Physics2D.OverlapPoint(pos + 2 * Vector2.left));
//
//
//
//
//           if (leftColl)
//           {
//               //Debug.Log("Left Collider" + leftColl.name);
//
//               //Debug.Log("Left collider instance ID " + leftColl.gameObject.GetInstanceID());
//               //Debug.Log("This instance ID " + gameObject.GetInstanceID());
//
//               //if (leftColl.GetComponentInParent<ElectricBlockBehavior>() && leftColl.gameObject.GetInstanceID() != gameObject.GetInstanceID())
//               //{
//               //    Debug.Log("Electric Block on left side" + transform.name);
//               //}
//
//               Debug.Log("Has Component " + leftColl.transform.root.GetComponent<ElectricBlockBehavior>());
//               Debug.Log(leftColl.transform.root.GetInstanceID() != this.transform.GetInstanceID());
//               if (leftColl.transform.root.GetComponent<ElectricBlockBehavior>() != null && leftColl.transform.root.GetInstanceID() != this.transform.GetInstanceID())
//               {
//                   Debug.Log("Left collider instance ID " + leftColl.gameObject.GetInstanceID());
//                   Debug.Log("This instance ID " + gameObject.GetInstanceID());
//                   ElectricBlockBehavior leftEBlock = leftColl.GetComponentInParent<ElectricBlockBehavior>();
//                   if (!connectedBlocks.Contains(leftEBlock))
//                   {
//                       Debug.Log("Add EBlock");
//                       connectedBlocks.Add(leftEBlock);
//
//                       if (leftEBlock.connectedLevers.Any())
//                           var _connectedLevers = leftEBlock.connectedLevers;
//                       connectedLevers.AddRange(_connectedLevers);
//                       Debug.Log("Connected Levers " + connectedLevers.Count);
//                       Debug.Log(connectedLevers[0]);
//                       Debug.Log(leftEBlock.connectedLevers[0]);
//                       foreach (ElectricLeverBhvr lever in connectedLevers)
//                       {
//                           lever.electricBlocks.Add(this);
//                       }
//                   }
//
//                   else if (leftEBlock.connectedLevers.Intersect(connectedLevers).Any())
//                   {
//                       RequestConnectionCheck();
//                   }
//
//               }
//               else if (leftColl.GetComponent<ElectricLeverBhvr>())
//               {
//                   Debug.Log("Lever detected");
//                   Debug.Log(leftColl.GetComponent<ElectricLeverBhvr>());
//                   if (!leftColl.GetComponent<ElectricLeverBhvr>().electricBlocks.Contains(this))
//                   {
//                       leftColl.GetComponent<ElectricLeverBhvr>().electricBlocks.Add(this);
//                       connectedLevers.Add(leftColl.GetComponent<ElectricLeverBhvr>());
//                   }
//
//               }
//           }
//
//
//           if (downColl)
//           {
//               if (downColl.GetComponent<ElectricBlockBehavior>() && downColl.GetComponent<ElectricBlockBehavior>() != this)
//               {
//                   ElectricBlockBehavior downEBlock = downColl.GetComponent<ElectricBlockBehavior>();
//                   if (!connectedBlocks.Contains(downEBlock) && downEBlock.connectedLevers.Count > 0)
//                   {
//                       connectedBlocks.Add(downEBlock);
//                       var _connectedLevers = downEBlock.connectedLevers;
//                       connectedLevers.AddRange(_connectedLevers);
//                       foreach (ElectricLeverBhvr lever in connectedLevers)
//                       {
//                           lever.electricBlocks.Add(this);
//                       }
//                   }
//                   else if (downEBlock.connectedLevers.Count == 0 && connectedBlocks.Count > 0)
//                   {
//
//                       RequestConnectionCheck();
//                   }
//
//               }
//               else if (downColl.GetComponent<ElectricLeverBhvr>())
//               {
//                   if (!downColl.GetComponent<ElectricLeverBhvr>().electricBlocks.Contains(this))
//                   {
//                       downColl.GetComponent<ElectricLeverBhvr>().electricBlocks.Add(this);
//                       connectedLevers.Add(downColl.GetComponent<ElectricLeverBhvr>());
//                   }
//               }
//           }
//
//           if (upColl)
//           {
//               if (upColl.GetComponent<ElectricBlockBehavior>() && upColl.GetComponent<ElectricBlockBehavior>() != this)
//               {
//                   ElectricBlockBehavior upEBlock = upColl.GetComponent<ElectricBlockBehavior>();
//
//                   if (!connectedBlocks.Contains(upEBlock) && upEBlock.connectedLevers.Count > 0)
//                   {
//                       connectedBlocks.Add(upEBlock);
//                       var _connectedLevers = upEBlock.connectedLevers;
//                       connectedLevers.AddRange(_connectedLevers);
//                       foreach (ElectricLeverBhvr lever in connectedLevers)
//                       {
//                           lever.electricBlocks.Add(this);
//                       }
//
//                   }
//                   else if (upEBlock.connectedLevers.Count == 0)
//                   {
//
//                       RequestConnectionCheck();
//                   }
//
//               }
//               else if (upColl.GetComponent<ElectricLeverBhvr>())
//               {
//                   if (!upColl.GetComponent<ElectricLeverBhvr>().electricBlocks.Contains(this))
//                   {
//                       upColl.GetComponent<ElectricLeverBhvr>().electricBlocks.Add(this);
//                       connectedLevers.Add(upColl.GetComponent<ElectricLeverBhvr>());
//
//                   }
//
//               }
//           }
//
//           if (rightColl)
//           {
//               if (rightColl.GetComponent<ElectricBlockBehavior>() && rightColl.GetComponent<ElectricBlockBehavior>() != this)
//               {
//                   ElectricBlockBehavior rightEBlock = rightColl.GetComponent<ElectricBlockBehavior>();
//
//                   if (!connectedBlocks.Contains(rightEBlock) && rightEBlock.connectedLevers.Count > 0)
//                   {
//                       connectedBlocks.Add(rightEBlock);
//                       var _connectedLevers = rightEBlock.connectedLevers;
//                       connectedLevers.AddRange(_connectedLevers);
//                       foreach (ElectricLeverBhvr lever in connectedLevers)
//                       {
//                           lever.electricBlocks.Add(this);
//                       }
//
//                   }
//                   else if (rightEBlock.connectedLevers.Count == 0)
//                   {
//
//                       RequestConnectionCheck();
//                   }
//
//               }
//               else if (rightColl.GetComponent<ElectricLeverBhvr>())
//               {
//                   if (!rightColl.GetComponent<ElectricLeverBhvr>().electricBlocks.Contains(this))
//                   {
//                       rightColl.GetComponent<ElectricLeverBhvr>().electricBlocks.Add(this);
//                       connectedLevers.Add(rightColl.GetComponent<ElectricLeverBhvr>());
//                   }
//
//               }
//           }
//
//
//       }
//
//       if (connectedLevers.Count < nbLeversInScene)
//       {
//           PlaceablePrefab.onPlaceEvent += OnPlace;
//       }
//       else
//       {
//           PlaceablePrefab.onPlaceEvent -= OnPlace;
//       }
//
//   }
//
    //    public void FindConnectedBlocks(List<ElectricBlockBehavior> electricBlocks)
    //    {
    //        Vector3Int pos = new Vector3Int((int)transform.position.x, (int)transform.position.y);
    //
    //        if (solidTm.GetTile(pos + Vector3Int.right) == electricTile)
    //        {
    //            electricBlocks.Add(GetBlock(pos + Vector3Int.right));
    //        }
    //        else if (GetBlock(pos + Vector3Int.right))
    //        {
    //            electricBlocks.Remove(GetBlock(pos + Vector3Int.right));
    //        }
    //      
    //        if (solidTm.GetTile(pos + Vector3Int.left) == electricTile)
    //        {
    //            electricBlocks.Add(GetBlock(pos + Vector3Int.left));
    //        }
    //        else if (GetBlock(pos + Vector3Int.left))
    //        {
    //            electricBlocks.Remove(GetBlock(pos + Vector3Int.left));
    //        }
    //      
    //        if (solidTm.GetTile(pos + Vector3Int.down) == electricTile)
    //        {
    //            electricBlocks.Add(GetBlock(pos + Vector3Int.down));
    //        }
    //        else if (GetBlock(pos + Vector3Int.down))
    //        {
    //            electricBlocks.Remove(GetBlock(pos + Vector3Int.down));
    //        }
    //        if (solidTm.GetTile(pos + Vector3Int.up) == electricTile)
    //        {
    //            electricBlocks.Add(GetBlock(pos + Vector3Int.up));
    //        }
    //        else if (GetBlock(pos + Vector3Int.up))
    //        {
    //            electricBlocks.Remove(GetBlock(pos + Vector3Int.up));
    //        }
    //
    //    }

   
//   ElectricBlockBehavior GetBlock(Vector3Int pos)
//   {
//       if (Physics2D.OverlapPoint(new Vector2(pos.x, pos.y)))
//       {
//           Collider2D eBlock = Physics2D.OverlapPoint(new Vector2(pos.x, pos.y));
//           return eBlock.gameObject.GetComponent<ElectricBlockBehavior>();
//       }
//       else return null;
//   }
//
//   //   public void ReceiveSignalState(int signalState)
    //   {
    //       if (signalState == 1)
    //       {
    //           electricFlowAnimator.enabled = true;
    //           //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    //           
    //       }
    //       else if (signalState == 0)
    //       {
    //           electricFlowAnimator.enabled = false;
    //           //gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    //       }
    //       _signalState = signalState;
    //       //TransmitSignalState(sender);
    //   }

    //    public void TransmitSignalState(GameObject sender)
    //    {
    //        foreach (ElectricBlockBehavior block in electricBlocks)
    //        {
    //            //block.ReceiveSignalState(_signalState, gameObject);
    //        }
    //    }

    //    private void OnTriggerEnter2D(Collider2D other)
    //    {
    //        if (other.GetComponent<ElectricBlockBehavior>() != null)
    //        {
    //            other.GetComponent<ElectricBlockBehavior>().enabled = false;
    //        }
    //    }

    //  private void OnTriggerExit2D(Collider2D other)
    //  {
    //      if (other.GetComponent<ElectricBlockBehavior>() != null)
    //      {
    //          electricBlocks.Remove(other.GetComponent<ElectricBlockBehavior>());
    //      }
    //  }



    // Levers send signals
//
//
//   public List<int> signals = new List<int>();
//   public void AddBlock(ElectricLeverBhvr lever)
//   {
//       connectedLevers.Add(lever);
//   }
//
//   public void UpdateSignal(ElectricLeverBhvr lever)
//   {
//       //signals[connectedLevers.IndexOf(lever)] = lever.leverState;
//   }
//
//   public void SetSignal()
//   {
//       GetComponent<Animator>().enabled = GetSignal();
//   }
//
//
//   bool GetSignal()
//   {
//       foreach (var signal in signals)
//       {
//           if (signal == 1)
//           {
//               return true;
//           }
//       }
//       return false;
//   }
//
//   public void SendToOther(ElectricLeverBhvr lever)
//   {
//       foreach (Transform transform in GetComponentsInChildren<Transform>())
//       {
//           if (transform.name == "Rotation Center" || transform == this.transform.root)
//           {
//               continue;
//           }
//
//           Vector2 pos = (Vector2)transform.position;
//           Collider2D rightColl = Physics2D.OverlapPoint(pos + Vector2.right);
//           Collider2D leftColl = Physics2D.OverlapPoint(pos + Vector2.left);
//           Collider2D downColl = Physics2D.OverlapPoint(pos + Vector2.down);
//           Collider2D upColl = Physics2D.OverlapPoint(pos + Vector2.up);
//
//           if (leftColl)
//           {
//               if (leftColl.gameObject.GetComponent<ElectricBlockBehavior>())
//               {
//                   ElectricBlockBehavior eBlock = leftColl.gameObject.GetComponent<ElectricBlockBehavior>();
//                   
//                   if (!eBlock.connectedLevers.Contains(lever))
//                   {
//                       eBlock.AddBlock(lever);
//                       eBlock.SendToOther(lever);
//                   }
//                   else
//                   {
//                       eBlock.SendToOther(lever);
//                   }
//
//               }
//           }
//
//
//           if (rightColl)
//           {
//               if (rightColl.gameObject.GetComponent<ElectricBlockBehavior>())
//               {
//                   ElectricBlockBehavior eBlock = rightColl.gameObject.GetComponent<ElectricBlockBehavior>();
//
//                   if (!eBlock.connectedLevers.Contains(lever))
//                   {
//                       eBlock.AddBlock(lever);
//                       eBlock.SendToOther(lever);
//                   }
//                   else
//                   {
//                       eBlock.SendToOther(lever);
//                   }
//
//               }
//           }
//
//
//           if (upColl)
//           {
//               if (upColl.gameObject.GetComponent<ElectricBlockBehavior>())
//               {
//                   ElectricBlockBehavior eBlock = upColl.gameObject.GetComponent<ElectricBlockBehavior>();
//
//                   if (!eBlock.connectedLevers.Contains(lever))
//                   {
//                       eBlock.AddBlock(lever);
//                       eBlock.SendToOther(lever);
//                   }
//                   else
//                   {
//                       eBlock.SendToOther(lever);
//                   }
//
//               }
//           }
//       }

        //List<ElectricBlockBehavior> GetBlocks(){
        //    
        //    List<ElectricBlockBehavior> eBlocks = new List<ElectricBlockBehavior>();
        //
        //    foreach ()
        //    Vector2 pos = (Vector2)transform.position;
        //    Collider2D rightColl = Physics2D.OverlapPoint(pos + Vector2.right);
        //    Collider2D leftColl = Physics2D.OverlapPoint(pos + Vector2.left);
        //    Collider2D downColl = Physics2D.OverlapPoint(pos + Vector2.down);
        //    Collider2D upColl = Physics2D.OverlapPoint(pos + Vector2.up);
        //
        //}
//    }


}



