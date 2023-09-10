using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmReceiver : MonoBehaviour
{
    public void ReceiveSignal(int signal)
    {
        if (signal == 1)
        {
            Debug.Log("Death");
        }
    }
}
