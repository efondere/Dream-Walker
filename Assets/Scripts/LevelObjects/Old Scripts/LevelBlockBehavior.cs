using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class LevelBlockBehavior: MonoBehaviour
{
    protected Rigidbody2D rb;
    protected BlockUpdate blockUpdate;

    public delegate void OnUpdate();
    public OnUpdate onUpdate;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //blockUpdate = new BlockUpdate(this);
        //blockUpdate.enabled = false;
    }

    
    public void GetSelectedPreBehaviors()
    {

    }

    public void GetSelectedPostBehaviors()
    {

    }

    public virtual void OnPlaced()
    {
        return;
    }

}



public class A : PropertyAttribute
{
    public int usageType;
    SerializedProperty property;
    public A(int usageType, SerializedProperty property)
    {
        this.usageType = usageType;
        this.property = property;
    }
}













// LevelBlockManager : make sure 



// Block Rotation
// Block




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
//   - carry prefab with movement
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
// method to add later

//public void Rotate(bool clockWise)
//{
//    float zRot = transform.rotation.z;
//    zRot += clockWise ? -90 : 90;
//    transform.rotation = Quaternion.Euler(0, 0, zRot);
//}



//    public void Fall()
//    {
//        rb.gravityScale = 2.5f;
//    }