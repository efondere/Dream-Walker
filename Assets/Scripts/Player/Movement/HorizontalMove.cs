using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class HorizontalMove : MonoBehaviour
{
    Rigidbody2D rb;
    private Jump jump;
    public float moveSpeed;

    // to prevent player from returning too rapidly to wall when jumping from wall
    [HideInInspector] public bool canHorizontalMove = true;

    private void Start()
    {
        jump = GetComponent<Jump>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveHorizontal(Vector2 moveDir)
    {
        if (canHorizontalMove)
        {
            if (!jump.hasWallJumped || moveDir != Vector2.zero)
            {
                rb.velocity = new Vector2(moveDir.x * moveSpeed, rb.velocity.y);
            }
        }
    }

    public IEnumerator StopHorizontalMovement()
    {
        canHorizontalMove = false;
        yield return new WaitForSeconds(0.5f);
        canHorizontalMove = true;
    }

}
  
 