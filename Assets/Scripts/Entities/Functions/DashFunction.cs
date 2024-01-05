using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class DashFunction : MonoBehaviour
{

    CollisionDetection collisionDetection;
    SpriteRenderer sr;
    [HideInInspector] public Rigidbody2D rb;
    HorizontalMovementFunction horizontalMove;
    public float dashSpeed;
    public float dashWait;
    private bool showGhost;
    [HideInInspector] public bool hasDashed;
    [HideInInspector] public bool isDashing;
    public Color dashColor;
    GhostTrail ghostTrail;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collisionDetection = GetComponent<CollisionDetection>();
        ghostTrail = GetComponent<GhostTrail>();
        sr = GetComponent<SpriteRenderer>();
        horizontalMove = GetComponent<HorizontalMovementFunction>();
    }
    private void Update()
    {
        if (showGhost)
        {
            ghostTrail.ShowGhost();
            showGhost = false;
        }

        if (hasDashed && collisionDetection.isGrounded())
        {
            hasDashed = false;
            sr.DOColor(Color.white, 0.1f);
            Debug.Log("Change Color to White");

        }
        else if (isDashing)
        {
            sr.DOColor(dashColor, 0.1f);
        }

        Debug.Log("Has Dashed = " + hasDashed);

        //    if (playCoroutine)
        //    {
        //        PlayCoroutine(_dashDir);
        //    }
    }

    //  public void PlayCoroutine(UnityEngine.Vector2 dashDir)
    //  {
    //      StartCoroutine(_Dash(dashDir));
    //  }

    //bool playCoroutine;
    //UnityEngine.Vector2 _dashDir;
    public void InitiateDash(UnityEngine.Vector2 dashDir)
    {
        if (!hasDashed && !isDashing)
        {
            //playCoroutine = true;
            if (dashDir != UnityEngine.Vector2.zero)
            {
                //_dashDir = dashDir.normalized;
                StartCoroutine(_Dash(dashDir.normalized));
            }
            else
            {
                StartCoroutine(_Dash(rb.velocity.normalized));
                //_dashDir = rb.velocity.normalized;

            }
            isDashing = true;
        }
    }

    private IEnumerator _Dash(UnityEngine.Vector2 dir)
    {
        rb.velocity = UnityEngine.Vector2.zero;
        rb.velocity += dir.normalized * dashSpeed;
        horizontalMove.canHorizontalMove = false;
        showGhost = true;
        rb.gravityScale = 0f;
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        yield return new WaitForSeconds(dashWait);
        isDashing = false;
        hasDashed = true;
        horizontalMove.canHorizontalMove = true;
        rb.gravityScale = 1f;
        //playCoroutine = false;
    }
}
