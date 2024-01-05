using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ElectricReceiverBhvr : MonoBehaviour
{
    public List<Transform> peripheralTransforms;
    [HideInInspector] public List<Vector2Int> peripheralPositions = new List<Vector2Int>();
    public UnityEvent receiverOnEvent;
    public UnityEvent receiverOffEvent;

    
    private void Awake()
    {
        foreach (var transform in peripheralTransforms)
        {
            peripheralPositions.Add(new Vector2Int((int)transform.position.x, (int)transform.position.y));

        }
    }

    public void ReceiveSignal(int signal)
    {
        if (signal == 1)
        {
            receiverOnEvent.Invoke();
        }
        else
        {
            receiverOffEvent.Invoke();
        }
    }
}
