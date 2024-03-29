using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    public Vector2 rightOffset;
    public Vector2 leftOffset;
    public Vector2 bottomOffset;
    public float collisionRadius;

    public bool isGrounded()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, LayerMask.GetMask("Ground"));

    }

    public bool touchWall()
    {
        return (Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, LayerMask.GetMask("Ground")) || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, LayerMask.GetMask("Ground")));
    }




    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + (Vector3)rightOffset, collisionRadius);
        Gizmos.DrawSphere(transform.position + (Vector3)leftOffset, collisionRadius);
        Gizmos.DrawSphere(transform.position + (Vector3)bottomOffset, collisionRadius);

    }


}
