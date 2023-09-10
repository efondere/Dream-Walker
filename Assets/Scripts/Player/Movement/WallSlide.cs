using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class WallSlide : MonoBehaviour
{
    Rigidbody2D rb;
    private Jump jump;
    private Dash dash;
    private CollisionDetection collisionDetection;
    public float slideSpeed;
    public Color wallSlideColor;
    private SpriteRenderer sr;
    private bool isWallSliding;

    private float _horizontalInput = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        jump = gameObject.GetComponent<Jump>();
        dash = gameObject.GetComponent<Dash>();
        collisionDetection = gameObject.GetComponent<CollisionDetection>();
        Debug.Log("dash " + dash == null);
    }

    // Update is called once per frame
    void Update()
    {
        InitiateWithCondition((Physics2D.OverlapCircle((Vector2)transform.position + collisionDetection.rightOffset, collisionDetection.collisionRadius, LayerMask.GetMask("Ground")) && (_horizontalInput > 0.1f))
                 || (Physics2D.OverlapCircle((Vector2)transform.position + collisionDetection.leftOffset, collisionDetection.collisionRadius, LayerMask.GetMask("Ground")) && (_horizontalInput < -0.1f)));

        Debug.Log("Is Wall Sliding = " + isWallSliding);
        if (!isWallSliding && sr.material.color == wallSlideColor)
        {
            Debug.Log("Wall Slide Color To Change");
            sr.material.DOColor(Color.white, 0.1f);
        }
    }

    public void SetHorizontalInput(float inputValue)
    {
        _horizontalInput = inputValue;
    }

    public void InitiateWithCondition(bool externalCondition)
    {
        if (externalCondition && !collisionDetection.isGrounded() && !dash.isDashing)
        {
            StartCoroutine(MeasureXVelocity());
            if (Mathf.Abs(xVelocity) <= 0.01f)
                StartCoroutine(_WallSlide());
        }
        else
        {
            isWallSliding = false;
        }


    }

    float xVelocity;
    private IEnumerator MeasureXVelocity()
    {
        float prevPosition = transform.position.x;
        yield return new WaitForSeconds(0.05f);
        xVelocity = (transform.position.x - prevPosition) / 0.05f;

    }

    private IEnumerator _WallSlide()
    {
        isWallSliding = true;

        if (jump.jumpFromGroundWait)
        {
            yield return new WaitForSeconds(1f);
            jump.jumpFromGroundWait = false;
        }
        else if (!jump.hasWallJumped) // if jump from wall, this allows the jump velocity to be set without keeping the slide velocity
        {
            sr.material.DOColor(wallSlideColor, 0.1f);
            rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);


        }
    }
}
