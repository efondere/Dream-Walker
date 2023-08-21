using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{

    // takes care of animations, and calling methods in Movement and EditingController scripts

    private float startTime_WrongSpawnPosAnimation = 1f;
    private float timeLeft_WrongSpawnPosAnimation = 1f;

    //  public int Lives;
    //   public GameObject[] healthUI;

    //  [HideInInspector] public bool shiftPressed = false;
    //  public playerBlocksManager playerBlocksManager;

    //   private Inputs inputs;
    //   private InputManager inputManager;
    //   private EditingController editingController;
    //   private Movement movementController;
    //   private CollisionDetection collisionDetection;
    //   private Vector3 mousePos;
    //   private Camera cam;

    // public float gravityScale;



    // private float startDeathTime = 0.5f;
    // private float deathTime;
    // public Animator animator;


    //  private void Start()
    //  {
    //      inputs = new Inputs();
    //      inputs.Enable();
    //
    //      inputManager = GetComponent<InputManager>();
    //      collisionDetection = GetComponent<CollisionDetection>();
    //      editingController = GetComponent<EditingController>();
    //      deathTime = startDeathTime;
    //      cam = Camera.main;
    //
    //      foreach (GameObject health in healthUI)
    //      {
    //          health.SetActive(true);
    //      }
    // }

///  private void Update()
///    {
/// if (!PauseMenu.isPaused)
///        {
///  playerBlocksManager.setDreaming(inputManager.IsEditing());
/// 
/// if (inputManager.IsEditing())
///            {
/// 
///  editingController.EditBlock();
///  animator.SetBool("IsEditing", true);
///            }           
/// else
///            {
///  animator.SetBool("IsEditing", false);
///            }
///        }
/// 
///        //Health ui + death
///  for (int i = 0; i < healthUI.Length; i++)
///        {
///  healthUI[i].SetActive(i < Lives);
///        }
/// 
/// if (Lives <= 0)
///        {
///  Die();
///        }
/// 
/// 
///    }

    //  private void 1FixedUpdate()
    //  {
    //
    //
    //
    //      //Locomotion
    //
    //      if (!PauseMenu.isPaused && !shiftPressed)
    //      {
    //          movementController.Move();
    //
    //          if (editingController.currentBlock != null)
    //          {
    //              editingController.currentBlock.SetActive(false);
    //              // editingController.animateToRed = false;
    //          }
    //      }
    //
    //
    //  }


    //  private void Move()
    //  {
    //
    //      moveDir = new Vector2(inputs.Movement.Horizontal.ReadValue<float>(), 0);
    //
    //
    //      if (inputs.Movement.Jump.IsPressed())
    //      {
    //
    //      }
    //
    //      AnimateMovement();
    //  }



  ///  void AnimateMovement()
  ///  {
  ///      if (moveDir.x < 0.0f)
  ///      {
  ///          GetComponent<SpriteRenderer>().flipX = true;
  ///      }
  ///      else if (moveDir.x > 0.0f) // prevent flipping when the player stops moving
  ///      {
  ///          GetComponent<SpriteRenderer>().flipX = false;
  ///      }
  ///
  ///      animator.SetFloat("xSpeed", Mathf.Abs(inputs.Movement.Horizontal.ReadValue<float>()));
  ///
  ///
  ///      if (yVelocity == 0f)
  ///      {
  ///          animator.SetBool("IsGrounded", true);
  ///      }
  ///      else
  ///      {
  ///          animator.SetBool("IsGrounded", false);
  ///
  ///      }
  ///      animator.SetFloat("yVelocity", yVelocity);
  ///
  ///  }
  ///
  ///
  ///
  ///  private void IndicateWrongSpawnPos()
  ///  {
  ///      SpriteRenderer currentBlockSpriteRenderer = currentBlock.GetComponent<SpriteRenderer>();
  ///      if (timeLeft_WrongSpawnPosAnimation >= 0.5f * startTime_WrongSpawnPosAnimation)
  ///      {
  ///          currentBlockSpriteRenderer.color = Color.Lerp(currentBlockSpriteRenderer.color, wrongSpawnPosColor, Time.deltaTime * animateToRedSpeed);
  ///          timeLeft_WrongSpawnPosAnimation -= Time.deltaTime;
  ///      }
  ///      else if (timeLeft_WrongSpawnPosAnimation <= 0.5f * startTime_WrongSpawnPosAnimation && timeLeft_WrongSpawnPosAnimation >= 0f)
  ///      {
  ///          currentBlockSpriteRenderer.color = Color.Lerp(currentBlockSpriteRenderer.color, new Color(1f, 1f, 1f, selectedTransparency), Time.deltaTime * animateToRedSpeed);
  ///          timeLeft_WrongSpawnPosAnimation -= Time.deltaTime;
  ///      }
  ///      else if (timeLeft_WrongSpawnPosAnimation <= 0f)
  ///      {
  ///          timeLeft_WrongSpawnPosAnimation = startTime_WrongSpawnPosAnimation;
  ///          animateToRed = false;
  ///
  ///      }
  ///  }
  ///
  ///
  ///  private void Die()
  ///  {
  ///
  ///      if (deathTime <= 0f)
  ///      {
  ///          animator.SetBool("IsDead", false);
  ///          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  ///      }
  ///      else
  ///      {
  ///          if (!animator.GetBool("IsDead"))
  ///          {
  ///              animator.SetBool("IsDead", true);
  ///              GetComponent<AudioSource>().Play();
  ///          }
  ///          deathTime -= Time.deltaTime;
  ///
  ///      }
  ///  }
}
