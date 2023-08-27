using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ElectricReceiverBhvr : MonoBehaviour
{
    [HideInInspector]public List<Vector2Int> peripheralPositions = new List<Vector2Int>();

    private void OnEnable()
    {
        peripheralPositions.Clear();
        Vector2Int pos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        peripheralPositions.Add(pos);
    }

    public UnityEvent receiverEvent;
    public void ReceiveSignal()
    {
        Debug.Log("receive");
        receiverEvent.Invoke();
    }
}
