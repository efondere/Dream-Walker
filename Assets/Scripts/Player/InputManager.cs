using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    public delegate void SetFallingCondition(bool conditionMet);

    private Inputs inputs;

    
    private CollisionDetection collisionDetection;
    public playerBlocksManager playerBlocksManager;
    private EditingController editingController;
    private HorizontalMove horizontalMove;
    private Jump jump;
    private Dash dash;
    private WallSlide wallSlide;

    

    [HideInInspector] private bool shiftActive;
    [HideInInspector] public bool canHorizontalMove = true;


    // Start is called before the first frame update
    void Start()
    {
        inputs = new Inputs();
        inputs.Enable();
        
        editingController =  GetComponent<EditingController>();
        collisionDetection = GetComponent<CollisionDetection>();
        horizontalMove = GetComponent<HorizontalMove>();
        jump = GetComponent<Jump>();
        dash = GetComponent<Dash>();
        wallSlide = GetComponent<WallSlide>();


    }

    private void Update()   
    {
        if (IsEditing())
        {
            editingController.EditBlock();
        }
        else if (editingController.currentBlock != null)
        {
            editingController.currentBlock.SetActive(false);
        }


            wallSlide.InitiateWithCondition(pushWall());

        horizontalMove.MoveHorizontal(HorizontalInput());

        if (JumpInput() >= 0)
        {
            if (jump.availableJumpType == 0 && JumpInput() == 1)
            {
                jump.InitiateJump(0);
            }
            else if (jump.availableJumpType == 1 && JumpInput() == 0)
            {
                jump.InitiateJump(1);
            }
            else if (jump.availableJumpType == 2 && JumpInput() == 0) {
                jump.InitiateJump(2);

            }
        }

        if (jump.canAlterUpwardMvt)
        {
            jump.AlterUpwardMovement(JumpInput() != 1);
        }

        if (DashPress())
        {
            Vector2 dashDir = new Vector2(HorizontalInput().x, VerticalInput().y);
            dash.InitiateDash(DashPress(), dashDir);
        }
    }

    public bool IsEditing()
    {
        if (!PauseMenu.isPaused && collisionDetection.isGrounded() && inputs.Editing.EditInit.WasPressedThisFrame())  
        {
            shiftActive = !shiftActive;
            playerBlocksManager.setDreaming(shiftActive);
        }
        return shiftActive;

    }

    public Vector2 MousePosition()
    {
        return inputs.Mouse.Pointer.ReadValue<Vector2>();
    }

    public bool MouseClick()
    {
        return inputs.Mouse.mouseClick.WasPressedThisFrame();
    }

    public bool RotatedBlock()
    {
        return inputs.Editing.Rotate.WasPressedThisFrame();
    }

    public float BlockRotationDirection()
    {
        return inputs.Editing.Rotate.ReadValue<float>();
    }

    public Vector2 HorizontalInput()
    {
        return new Vector2(inputs.Movement.Horizontal.ReadValue<float>(), 0f);
    }

    public Vector2 VerticalInput()
    {
        return new Vector2(0, inputs.Movement.Vertical.ReadValue<float>());
    }
    
    public int JumpInput()
    {
        if (inputs.Movement.Jump.WasPressedThisFrame())
            return 0;
        else if (inputs.Movement.Jump.IsPressed())
            return 1;

        return -1;
    }

    public bool DashPress()
    {
        return inputs.Movement.Dash.WasPressedThisFrame();
    }

    private bool pushWall() {
        return (Physics2D.OverlapCircle((Vector2)transform.position + collisionDetection.rightOffset, collisionDetection.collisionRadius, LayerMask.GetMask("Ground")) && (HorizontalInput().x > 0.1f))
                 || (Physics2D.OverlapCircle((Vector2)transform.position + collisionDetection.leftOffset, collisionDetection.collisionRadius, LayerMask.GetMask("Ground")) && (HorizontalInput().x < -0.1f));

    }

}
