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
    private GameObject lastCurrentBlock;
    public playerBlocksManager playerBlocksManager;
    int currentBlockIndex = 0;
    public float blockMoveStep;
    //private bool droppedBlock = false;
    [Space]
    public Color normalColor;
    private float currentBlockAngle;
    //private float startMinTimeBtwBlockDrops = 0.5f;
    //private float timeBtwBlockDrops = 0.5f;

    private float startTimerBtwKeyPress = 0.2f;
    private float timeBtwKeyPress = 0.2f;

    private float startTime_WrongSpawnPosAnimation = 1f;
    private float timeLeft_WrongSpawnPosAnimation = 1f;
    [Space]
    public float selectedTransparency;
    public Color wrongSpawnPosColor;
    public float animateToRedSpeed;
    [Space]
    private bool animateToRed;
    public RectTransform blockSpawnBound;
    [Space]
    public int Lives;
    private bool  shiftPressed = false;

    public InputAction mouseClick;

    private Vector3 mousePos;
    private Camera cam;

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
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded())
        {
            shiftPressed = !shiftPressed;
            playerBlocksManager.setDreaming(shiftPressed);
        }
        if (shiftPressed)
        {
            EditBlock();
        }

    }
    private void FixedUpdate()
    {
        moveDir = new Vector2(Input.GetAxis("Horizontal"), 0);

        
        if (!shiftPressed)
        {
            animateToRed = false;
         //   if (droppedBlock)
         //   {
         //       if (timeBtwBlockDrops <= 0f) {
         //           droppedBlock = false;
         //           timeBtwBlockDrops = startMinTimeBtwBlockDrops;
         //       }
         //       else
         //       {
         //           timeBtwBlockDrops -= Time.deltaTime;
         //       }

          //  }

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

      //   if (!isGrounded())
      //   {
      //       gameObject.GetComponent<PolygonCollider2D>().enabled = false;
      //   }
      //   else
      //   {
      //       gameObject.GetComponent<PolygonCollider2D>().enabled = true;
      //
      //    }

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
            if (moveDir.x < 0.0f)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (moveDir.x > 0.0f) // prevent flipping when the player stops moving
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

    }


    private void EditBlock()
    {
        
        if (currentBlockIndex != playerBlocksManager.m_currentlySelectedTile)
        {
            if (currentBlock != null)
            {
                currentBlock.SetActive(false);
            }
            currentBlockIndex = playerBlocksManager.m_currentlySelectedTile;
        }
        if (playerBlocksManager.blockList[currentBlockIndex].Count != 0)
        {
            currentBlock = playerBlocksManager.blockList[currentBlockIndex][0];
            currentBlock.SetActive(true);
            Collider2D currentBlockCollider = currentBlock.GetComponent<Collider2D>();
            currentBlockCollider.isTrigger = true;
            SpriteRenderer currentBlockSpriteRenderer = currentBlock.GetComponent<SpriteRenderer>();
            currentBlockSpriteRenderer.color = new Color(currentBlockSpriteRenderer.color.r, currentBlockSpriteRenderer.color.g,currentBlockSpriteRenderer.color.b, 0.2f);
            currentBlock.transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * blockMoveStep;
            mousePos = Input.mousePosition;
            currentBlock.transform.position = cam.ScreenToWorldPoint(mousePos) + new Vector3(0f,0f,10f);

            if (animateToRed)
            {
                IndicateWrongSpawnPos();
            }
            if (timeBtwKeyPress <= 0f)
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    currentBlockAngle += 90f;
                    currentBlock.transform.rotation = Quaternion.Euler(0f, 0f, currentBlockAngle);
                }
                else if (Input.GetKey(KeyCode.E))
                {

                    currentBlockAngle -= 90f;
                    currentBlock.transform.rotation = Quaternion.Euler(0f, 0f, currentBlockAngle);
                }
                timeBtwKeyPress = startTimerBtwKeyPress;
            }
            else
            {
                timeBtwKeyPress -= Time.deltaTime;
            }


            if (mouseClick.WasPressedThisFrame())
            {
                //Collider2D otherCollider = currentBlockCollider.OverlapBox(currentBlock.transform.position, new Vector2(currentBlockCollider.size.x * currentBlock.transform.lossyScale.x, currentBlockCollider.size.y * currentBlock.transform.lossyScale.y), 0f);
                if (!currentBlockCollider.IsTouchingLayers())
                {
                    currentBlockSpriteRenderer.color = normalColor;
                    currentBlockCollider.isTrigger = false;
                    playerBlocksManager.blockList[currentBlockIndex].Remove(currentBlock);
                    playerBlocksManager.useBlock(currentBlockIndex);
                    if (playerBlocksManager.blockList[currentBlockIndex].Count != 0)
                    {
                        currentBlock = playerBlocksManager.blockList[currentBlockIndex][0];
                    }
                    else
                    {
                        currentBlock = null;
                    }
                // droppedBlock = true;
                }
                else
                {
                    animateToRed = true;
                }


            }

            if (currentBlock != null)
            {
                if (currentBlock.transform.position.x - currentBlock.transform.lossyScale.x * 0.5f < blockSpawnBound.position.x - blockSpawnBound.rect.width * 0.5f)
                {
                    currentBlock.transform.position = new Vector3(blockSpawnBound.position.x - blockSpawnBound.rect.width * 0.5f + currentBlock.transform.lossyScale.x * 0.5f, 0f, 0f);
                }
                if (currentBlock.transform.position.x + currentBlock.transform.lossyScale.x * 0.5f > blockSpawnBound.position.x + blockSpawnBound.rect.width * 0.5f)
                {
                    currentBlock.transform.position = new Vector3(blockSpawnBound.position.x + blockSpawnBound.rect.width * 0.5f - currentBlock.transform.lossyScale.x * 0.5f, 0f, 0f);

                }
                if (currentBlock.transform.position.y - currentBlock.transform.lossyScale.y * 0.5f < blockSpawnBound.position.y - blockSpawnBound.rect.height * 0.5f)
                {
                    currentBlock.transform.position = new Vector3(blockSpawnBound.position.y - blockSpawnBound.rect.height * 0.5f + currentBlock.transform.lossyScale.x * 0.5f, 0f, 0f);
                }
                if (currentBlock.transform.position.y + currentBlock.transform.lossyScale.y * 0.5f > blockSpawnBound.position.y + blockSpawnBound.rect.height * 0.5f)
                {
                    currentBlock.transform.position = new Vector3(blockSpawnBound.position.y + blockSpawnBound.rect.height * 0.5f - currentBlock.transform.lossyScale.y*0.5f,0f,0f);
                }
            }
        }
        else
        {
            currentBlock = null;
        }  
    }

   private void IndicateWrongSpawnPos()
    {
        SpriteRenderer currentBlockSpriteRenderer = currentBlock.GetComponent<SpriteRenderer>();
        if (timeLeft_WrongSpawnPosAnimation >= 0.5f*startTime_WrongSpawnPosAnimation)
        {
            currentBlockSpriteRenderer.color = Color.Lerp(currentBlockSpriteRenderer.color, wrongSpawnPosColor, Time.deltaTime * animateToRedSpeed);
            timeLeft_WrongSpawnPosAnimation -= Time.deltaTime;
        }
        else if (timeLeft_WrongSpawnPosAnimation <= 0.5f*startTime_WrongSpawnPosAnimation && timeLeft_WrongSpawnPosAnimation >= 0f)
        {
            currentBlockSpriteRenderer.color = Color.Lerp(currentBlockSpriteRenderer.color, new Color(1f,1f,1f,selectedTransparency), Time.deltaTime * animateToRedSpeed);
            timeLeft_WrongSpawnPosAnimation -= Time.deltaTime;
        }
        else if (timeLeft_WrongSpawnPosAnimation <= 0f) {
            timeLeft_WrongSpawnPosAnimation = startTime_WrongSpawnPosAnimation;
            animateToRed = false;

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
