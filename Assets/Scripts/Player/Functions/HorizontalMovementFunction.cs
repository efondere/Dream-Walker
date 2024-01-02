using System.Collections;
using UnityEngine;

[RequireComponent(typeof(JumpFunction))]
[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalMovementFunction : MonoBehaviour
{
    private Rigidbody2D _rb;
    private JumpFunction _jump;

    public float walkSpeed;
    public float runSpeed;

    // to prevent player from returning too rapidly to wall when jumping from wall
    [HideInInspector] public bool canHorizontalMove = true;

    private void Start()
    {
        _jump = GetComponent<JumpFunction>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void MoveHorizontal(Vector2 moveDir)
    {
        if (canHorizontalMove)
        {
            if (!_jump.hasWallJumped || moveDir != Vector2.zero)
            {
                _rb.velocity = new Vector2(moveDir.x * walkSpeed, _rb.velocity.y);
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


