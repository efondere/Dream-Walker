using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D groundChecker;
    public float moveSpeed;
    public float jumpVelocity;
    public float gravityScale;
    private bool isJumping; 

    Vector2 moveDir;
    float yVelocity = 0;
    private GameObject currentBlock;
    public playerBlocksManager playerBlocksManager;
    int currentBlockIndex = 0;
    public float blockMoveStep;
    private bool droppedBlock = false;
    private float startTimeBtwClicks;
    private float timeBtwClicks;
    public InputAction mouseClick;

    private void OnEnable()
    {
        mouseClick.Enable();
    }
    private void OnDisable()
    {
        mouseClick.Disable();
    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        groundChecker = gameObject.GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        moveDir = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (!isGrounded())
        {
            yVelocity -= Time.fixedDeltaTime * gravityScale;
        }
        else if (!isJumping)
        {
            yVelocity = 0f;
        }
        else if (isJumping)
        {
            isJumping = false;

        }

        if (!Input.GetButton("Fire3") || droppedBlock)
        {
            droppedBlock = false;
            if (currentBlock != null)
            {
                currentBlock.SetActive(false);
            }
            if (Input.GetButton("Jump"))
            {
                if (isGrounded())
                {
                    isJumping = true;
                    yVelocity = jumpVelocity;
                }

            }
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime + Vector2.down * gravityScale * 0.5f * Mathf.Pow(Time.fixedDeltaTime, 2) + Vector2.up * yVelocity * Time.fixedDeltaTime);
        }
        else
        {
            EditBlock();
        }
    }


    private void EditBlock()
    {
        currentBlock = playerBlocksManager.blockList[currentBlockIndex][0];
        currentBlock.SetActive(true);
        currentBlock.transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * blockMoveStep;

        if (mouseClick.IsPressed())
        {
          playerBlocksManager.blockList[currentBlockIndex].Remove(currentBlock);
          currentBlock = playerBlocksManager.blockList[currentBlockIndex][0];
          droppedBlock = true;
           
        }
        if (Input.GetKey(KeyCode.Z))
        {
            if (currentBlockIndex == playerBlocksManager.blocks.Length - 1)
            {
                currentBlockIndex = 0;
            }
            else
            {
                currentBlockIndex++;
                currentBlock.SetActive(false);
            }
        }
    }
    private bool isGrounded()
    {
        if (groundChecker.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return true;

        }
        else
        {
            return false;
        }
    }
}
