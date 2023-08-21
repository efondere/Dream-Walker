using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using System.Linq;
using System.Threading;

public class LinearMove : MonoBehaviour
{
    // variables to set
    private Transform[] targets;
    private float waitSecondsAtTarget;
    private bool loop;
    private float speed;

    // etc.
    private int currentTarget = 0;
    private int lastTarget = 0;
    private Rigidbody2D rb;
    
    BlockBehaviorManager blockBehaviorManager;

    public void Start()
    {
        blockBehaviorManager = BlockBehaviorManager.GetInstance();
        Debug.Log(blockBehaviorManager.GetInstanceID());
        rb = GetComponent<Rigidbody2D>();

    }

    public void SetAndStart(LinearMoveData data)
    {
        targets = data.targets;
        lastTarget = targets.Length - 1;
        loop = data.loop;
        speed = data.speed;
        BlockBehaviorManager.StartBehavior(MoveLinearly);
    }

    public void MoveLinearly()
    {
        if (transform.position == targets[currentTarget].position)
        {
            if (currentTarget != lastTarget)
            currentTarget++;
            else if (!loop)
            {
                BlockBehaviorManager.StopBehavior(MoveLinearly);
            }
            else
            {
                currentTarget = 0;
                targets.Reverse();
            }

        }
        else if (currentTarget <= lastTarget)
        {
            rb.DOMove(targets[currentTarget].position, Vector3.Distance(transform.position, targets[currentTarget].position) / speed);
        }
    }
}