using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.Windows;
using System;
using JetBrains.Annotations;

public class Jump : MonoBehaviour
{
    Rigidbody2D rb;
    CollisionDetection collisionDetection;
    HorizontalMove horizontalMove;
    public float jumpForce;
    public float jumpOffWallSpeed;
    public float isFallingMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;
    public float coyoteTime;

    [HideInInspector] public int availableJumpType; // 0 = normal jump; 1 = coyote jump; 2 = wall jump

    // to avoid : jittery behavior when jumping next to a wall (returning to ground immediately after jump because of wallslide)
    [HideInInspector] public bool jumpFromGroundWait;

    [HideInInspector] public bool hasWallJumped = false;
    private bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collisionDetection = GetComponent<CollisionDetection>();
        horizontalMove = GetComponent<HorizontalMove>();
        availableJumpType = -1;
    }

    private void Update()
    {
        CheckJumpConditions();

        if (isJumping && rb.velocity.y < -0.01f) // applies to all objects in the same way
        AlterDownwardMovement();

        if (rb.velocity.y > 0.01f)
        {
            canAlterUpwardMvt = true;
        }
    }

    public void AlterDownwardMovement()
    {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (isFallingMultiplier - 1f) * Time.deltaTime;
            jumpFromGroundWait = false;
            if (collisionDetection.isGrounded())
            {
                hasWallJumped = false;
                isJumping = false;
            }
    }

    [HideInInspector]public bool canAlterUpwardMvt;
    public void AlterUpwardMovement(bool conditionMet)
    {
        if (conditionMet)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
        }
    }

    public float percentOflayerHeight;
    public void CheckJumpConditions()
    {
        if (!collisionDetection.isGrounded() && !isJumping)
        {
            StartCoroutine(CoyoteJump(coyoteTime));
            if (canCoyoteJump)
                availableJumpType = 1;
        }
        else if (collisionDetection.isGrounded())
        {
            if ((transform.position.y - percentOflayerHeight * transform.lossyScale.y) > Physics2D.OverlapCircle((Vector2)transform.position + collisionDetection.bottomOffset, collisionDetection.collisionRadius, LayerMask.GetMask("Ground")).gameObject.transform.position.y)
            {
                availableJumpType = 0;          
            }
            Debug.Log("Is Grounded");
        }
        else if (collisionDetection.touchWall())
        {
            availableJumpType = 2;
        }
        else
        {
            availableJumpType = -1;
        }

        Debug.Log("available jump type = " + availableJumpType);
    }


    public void InitiateJump(int jumpType)
    {
        if (jumpType == 0)
        {
            jumpFromGroundWait = true;
            _Jump(Vector2.up);
        }
        else if (jumpType == 1)
        {
            _Jump(Vector2.up);
        }
        else if (jumpType == 2)
        {
            int side = Physics2D.OverlapCircle((Vector2)transform.position + collisionDetection.rightOffset, collisionDetection.collisionRadius, LayerMask.GetMask("Ground")) ? 1 : -1;
            _Jump(new Vector2(-side, 1).normalized);
            Debug.Log(new Vector2(-side, 1).normalized);
            hasWallJumped = true;
            StartCoroutine(horizontalMove.StopHorizontalMovement());
        }
    }


    bool canCoyoteJump;
    private IEnumerator CoyoteJump(float coyoteTime)
    {
        canCoyoteJump = true;
        yield return new WaitForSeconds(coyoteTime);
        canCoyoteJump = false;
    }

    private void _Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.velocity += dir * jumpForce;
        isJumping = true;
    }
}
