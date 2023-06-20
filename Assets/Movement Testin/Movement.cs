using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float gravityScale;
    public float moveSpeed;
    public float jumpForce;
    public float slideSpeed;
    public float jumpOffWallSpeed;
    public float isFallingMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;
    public float dashSpeed;
    public float dashWait;
    // to avoid : jittery behavior when jumping next to a wall (returning to ground immediately after jump because of wallslide)
    private bool jumpFromGroundWait;

    // to prevent player from returning too rapidly to wall when jumping from wall
    private bool canMove = true;
    private bool hasWallJumped = false;
    private bool hasJumped = false;

    private bool showGhost;
    private bool hasDashed;

    private Vector2 moveDir = Vector2.zero;
    private Vector2 dashDir = Vector2.zero;

    public Color dashColor;
    public Color wallSlideColor;

    // on Wall Collider
    public Vector2 rightOffset;
    public Vector2 leftOffset;
    public Vector2 bottomOffset;
    public float collisionRadius;

    private Inputs inputs;
    private GhostTrail ghostTrail;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        inputs = new Inputs();
        inputs.Movement.Enable();
        rb.gravityScale = gravityScale;
        ghostTrail = this.gameObject.GetComponent<GhostTrail>();

    }

    // Update is called once per frame
    void Update()
    {
        #region Horizontal move
        if (canMove)
        {
           moveDir = new Vector2(inputs.Movement.Horizontal.ReadValue<float>(), 0f);
           Walk(moveDir);
           Debug.Log("IsWalking");
           Debug.Log("Horizontal : " + inputs.Movement.Horizontal.ReadValue<float>());
       }
        #endregion

        #region Jump

        if (inputs.Movement.Jump.IsPressed() && isGrounded())
        {
            Jump(Vector2.up);
            jumpFromGroundWait = true;


        }
        else if ((pushWall() || touchWall()) && inputs.Movement.Jump.WasPressedThisFrame())
        {
            int side = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, LayerMask.GetMask("Ground")) ? 1 : -1;
            Jump(new Vector2(-side, 1).normalized);
            Debug.Log(new Vector2(-side, 1).normalized);
            hasWallJumped = true;
            StartCoroutine(StopMovement());

        }
        else if (rb.velocity.y < -0.01f)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (isFallingMultiplier - 1f) * Time.deltaTime;
            jumpFromGroundWait = false;
            if (isGrounded())
            {
                hasWallJumped = false;
            }
        }
        else if (rb.velocity.y > 0.01f && !inputs.Movement.Jump.IsPressed())
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
            Debug.Log("wOW");
        }

        #endregion

        #region wall slide
        // wall slide
        if (pushWall() && !isGrounded()) {

            Debug.Log("Wall Slide");
            StartCoroutine(WallSlide());

        }
        else if (sr.material.color == wallSlideColor)
        {
            Debug.Log("Wall Slide Color To Change");
            sr.material.DOColor(Color.white, 0.1f);
        }
        #endregion

        #region dash
        if (inputs.Movement.Dash.WasPressedThisFrame())
        {
            Vector2 dashDir = new Vector2(inputs.Movement.Horizontal.ReadValue<float>(), inputs.Movement.Vertical.ReadValue<float>());
            
            if (dashDir != Vector2.zero) {
                StartCoroutine(Dash(dashDir.normalized));
            }
            else
            {
                StartCoroutine(Dash(rb.velocity.normalized));
            }

            hasDashed = true;

        }

        if (showGhost)
        {
            ghostTrail.ShowGhost();
            showGhost = false;
        }

        if (hasDashed && isGrounded())
        {
            hasDashed = false;
            sr.DOColor(Color.white, 0.1f);

        }
        else if (hasDashed)
        {
            sr.DOColor(dashColor, 0.1f);
        }
        #endregion


    }

    private IEnumerator StopMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

    private IEnumerator Dash(Vector2 dir)
    {
        rb.velocity = Vector2.zero;
        rb.velocity += dir.normalized * dashSpeed;
        canMove = false;
        showGhost = true;
        rb.gravityScale = 0f;
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        yield return new WaitForSeconds(dashWait);
        canMove = true;
        rb.gravityScale = gravityScale;
    }

    private void Walk(Vector2 moveDir)
    {
        if (!hasWallJumped || moveDir != Vector2.zero)
        {
            rb.velocity =  new Vector2(moveDir.x * moveSpeed, rb.velocity.y);
        }
    }
    private void Jump(Vector2 dir)
    {

        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.velocity += dir * jumpForce;
        Debug.Log("Jump direction + " + dir);
    }

    private IEnumerator WallSlide()
    {
        if (jumpFromGroundWait)
        {
            yield return new WaitForSeconds(1f);
            jumpFromGroundWait = false;
        }
        else if (!inputs.Movement.Jump.IsPressed())
        {
            sr.material.DOColor(wallSlideColor, 0.1f);
            rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);


        }
    }


    //Collision detection

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, LayerMask.GetMask("Ground"));
            
    }

    private bool pushWall()
    {
        return (Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, LayerMask.GetMask("Ground")) && (moveDir.x > 0.1f))
            || (Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, LayerMask.GetMask("Ground")) && (moveDir.x < -0.1f));
    }
    
    private bool touchWall()
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



// In the update method

//    if (onWall())
//    {
//        if (Input.GetKey(KeyCode.M))
//        {
//            rb.velocity = new Vector2(rb.velocity.x, inputs.Movement.Vertical.ReadValue<float>() * climbSpeed);
//            isClimbing = true;
//            Debug.Log("Climb");
//        }
//    }
//    else if (isClimbing)
//    {
//        isClimbing = false;
//    }







