using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening.Plugins;

public class playerController1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D groundChecker;
    public float moveSpeed;
    public float jumpVelocity;
    public float gravityScale;
    private bool isJumping;
    public float isFallingMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;
    [HideInInspector] public Vector2 moveDir;
    [HideInInspector] public float yVelocity = 0;

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
    public GameObject[] healthUI;

    [HideInInspector]public bool  shiftPressed = false;

    private Inputs inputs;

    private Vector3 mousePos;
    private Camera cam;


    private float startDeathTime = 0.5f;
    private float deathTime;
    public Animator animator;


    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable();


        deathTime = startDeathTime;
        rb = gameObject.GetComponent<Rigidbody2D>();
        groundChecker = gameObject.GetComponent<BoxCollider2D>();
        cam = Camera.main;

        foreach (GameObject health in healthUI)
        {
            health.SetActive(true);
        }
    }

    private void Update()
    {
        if (!PauseManager.IsPaused())
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded())
            {
                shiftPressed = !shiftPressed;
                playerBlocksManager.setDreaming(shiftPressed);
            }
            if (shiftPressed)
            {
                EditBlock();
                animator.SetBool("IsEditing", shiftPressed);
            }
            else
            {
                animator.SetBool("IsEditing", shiftPressed);

            }


        }


    }

    private void FixedUpdate()
    {
        
        //Health ui + death
        for (int i = 0; i < healthUI.Length; i++)
        {
            healthUI[i].SetActive(i < Lives);
        }

        if (Lives <= 0)
        {
            Die();
        }

        //Locomotion

        if (!isJumping)
        {
            ApplyGravity(gravityScale);
        }
        if (!PauseManager.IsPaused() && !shiftPressed)
        {
            Move();

            if (currentBlock != null)
            {
                currentBlock.SetActive(false);
                animateToRed = false;
            }
        }
        else
        {
            moveDir = Vector2.zero;
        }
    }


    private void Move()
    {

        // moveDir = new Vector2(inputs.Movement.Horizontal.ReadValue<float>(), 0);    

        if (!isJumping && isGrounded())
        {
                yVelocity = 0f;
        }

        // if (inputs.Movement.Jump.IsPressed())
        // {
        //     if (isGrounded())
        //     {
        //         yVelocity = jumpVelocity;
        //         isJumping = true;
        //
        //     }
        //
        // }

        if (isJumping)
        {

            if (yVelocity < 0f)
            {
                ApplyGravity(gravityScale * (isFallingMultiplier - 1f));

                if (isGrounded())
                {
                    isJumping = false;
                }
            }
            // else if (yVelocity > 0f && inputs.Movement.Jump.IsPressed())
            // {
            //     ApplyGravity(gravityScale * (lowJumpMultiplier - 1f));
            //
            //     Debug.Log("gravityScale : " + gravityScale * (lowJumpMultiplier - 1f));
            //
            // }
            else
            {
                ApplyGravity(gravityScale);
            }


        }


        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime + Vector2.up * yVelocity * Time.fixedDeltaTime);



        AnimateMovement();


    }

    private void ApplyGravity(float gravity)
    {
        if (!isGrounded())
        {         
            yVelocity -= Time.fixedDeltaTime * gravity;
            rb.MovePosition(rb.position + Vector2.down * gravity * 0.5f * Mathf.Pow(Time.fixedDeltaTime, 2) + Vector2.up * yVelocity * Time.fixedDeltaTime);

        }
    }


    void AnimateMovement()
    {
        if (moveDir.x < 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveDir.x > 0.0f) // prevent flipping when the player stops moving
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // animator.SetFloat("xSpeed", Mathf.Abs(inputs.Movement.Horizontal.ReadValue<float>()));


        if (yVelocity == 0f)
        {
            animator.SetBool("IsGrounded", true);
        }
        else
        {
            animator.SetBool("IsGrounded", false);

        }
        animator.SetFloat("yVelocity", yVelocity);

    }

    private void EditBlock()
    {

        // Change selection
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
            //Get and activate block
            currentBlock = playerBlocksManager.blockList[currentBlockIndex][0];
            currentBlock.SetActive(true);

            //Get block components
            Collider2D currentBlockCollider = currentBlock.GetComponent<Collider2D>();
            currentBlockCollider.isTrigger = true;
            SpriteRenderer currentBlockSpriteRenderer = currentBlock.GetComponent<SpriteRenderer>();

            //Set block transparency for editing
            currentBlockSpriteRenderer.color = new Color(currentBlockSpriteRenderer.color.r, currentBlockSpriteRenderer.color.g, currentBlockSpriteRenderer.color.b, 0.2f);


            //currentBlock.transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * blockMoveStep;

            // Block control

            // mousePos = inputs.Mouse.Pointer.ReadValue<Vector2>();
            if (!PauseManager.IsPaused())
            {
                currentBlock.transform.position = cam.ScreenToWorldPoint(mousePos) + new Vector3(0f, 0f, 10f);
            }

            // if (inputs.Editing.Rotate.WasPressedThisFrame() && inputs.Editing.Rotate.ReadValue<float>() > 0f)
            // {
            //     currentBlockAngle += 90f;
            //     currentBlock.transform.rotation = Quaternion.Euler(0f, 0f, currentBlockAngle);
            // }
            // else if (inputs.Editing.Rotate.WasPressedThisFrame()&&inputs.Editing.Rotate.ReadValue<float>() < 0f)
            // {
            //
            //     currentBlockAngle -= 90f;
            //     currentBlock.transform.rotation = Quaternion.Euler(0f, 0f, currentBlockAngle);
            // }


            // Drop block
            // if (inputs.Mouse.mouseClick.WasPressedThisFrame() && !PauseManager.IsPaused())
            // {
            //     //Collider2D otherCollider = currentBlockCollider.OverlapBox(currentBlock.transform.position, new Vector2(currentBlockCollider.size.x * currentBlock.transform.lossyScale.x, currentBlockCollider.size.y * currentBlock.transform.lossyScale.y), 0f);
            //     if (!currentBlockCollider.IsTouchingLayers(-1))
            //     {
            //         currentBlockSpriteRenderer.color = normalColor;
            //         currentBlockCollider.isTrigger = false;
            //         playerBlocksManager.blockList[currentBlockIndex].Remove(currentBlock);
            //         playerBlocksManager.useBlock(currentBlockIndex);
            //
            //         if (playerBlocksManager.blockList[currentBlockIndex].Count != 0)
            //         {
            //             currentBlock = playerBlocksManager.blockList[currentBlockIndex][0];
            //         }
            //         else
            //         {
            //             currentBlock = null;
            //         }
            //     // droppedBlock = true;
            //     }
            //     else
            //     {
            //         animateToRed = true;
            //     }
            //
            //
            //
            // }


            // Indicating wrong spawn pos
            if (animateToRed)
            {
                IndicateWrongSpawnPos();
            }


            // Setting bounds for spawning
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


    private void Die()
    {

        if (deathTime <= 0f)
        {
            animator.SetBool("IsDead", false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            if (!animator.GetBool("IsDead"))
            {
                animator.SetBool("IsDead", true);
                GetComponent<AudioSource>().Play();
            }
            deathTime -= Time.deltaTime;

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
