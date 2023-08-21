using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ElectricReceiverBhvr : MonoBehaviour
{
    public UnityEvent receiverEvent;
    public void ReceiveSignal()
    {
        receiverEvent.Invoke();
    }
}
