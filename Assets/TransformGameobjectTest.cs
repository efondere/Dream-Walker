using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransformGameobjectTest : MonoBehaviour
{
    public Transform customTransform;

    private void OnDrawGizmos()
    {

        Gizmos.DrawLine(new Vector3(-(int)customTransform.lossyScale.x / 2, 0, 0), new Vector3((int)customTransform.lossyScale.x / 2, 0, 0));
    }
}
