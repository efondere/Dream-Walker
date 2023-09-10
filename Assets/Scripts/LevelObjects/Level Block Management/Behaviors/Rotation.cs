using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public void Rotate(RotationData rotationData)
    {
        int direction = rotationData.clockWise ? -1 : 1;
        Vector3 rotationCenter = new Vector3(rotationData.rotationCenter.position.x, rotationData.rotationCenter.position.y, 0);
        gameObject.transform.RotateAround(rotationCenter,Vector3.forward, direction * 90);
    }
}

