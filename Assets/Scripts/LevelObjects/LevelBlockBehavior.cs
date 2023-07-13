using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LevelBlockBehaviour : MonoBehaviour
{
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    public void Rotate(bool clockWise)
    {
        float zRot = transform.rotation.z;
        zRot += clockWise ? -90 : 90;
        transform.rotation = Quaternion.Euler(0, 0, zRot);
    }

    public void Fall()
    {
        
    }

    public void LinearMove(Vector2Int target, bool stopAtTarget)
    {

    }
}



public class TrailHolderManager
{
    

}

public delegate void MethodDone(int i);

class MethodHolder
{
    public static event MethodDone a;

    
    public void MethodDDone(int i)
    {
        // do something
    }
}

// block behaviors: 
// - move (only smoothdamp)
// - prevent collision with placed block
// - collide with placed blocks
//   - disappear with anim
//   - explode and disappear
//   - disappear and return to initial position
//
//
// Zone behavior
// - Movement
//   - stationary
//   - bounce
//   - waypoints
//   - random shape, etc.
// - Event trigger (use color code or signs?)
//   - falling block
//   - moving block
//   - rotate block
//   - open new area
//   - cause mov
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
