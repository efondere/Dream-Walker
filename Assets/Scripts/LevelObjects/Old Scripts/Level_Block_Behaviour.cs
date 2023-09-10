using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Block_Behaviour : MonoBehaviour
{

    public bool canDisappear;
    public float startTimeBeforeDisap;
    private float timeBeforeDisap;
    public bool disappearPermanently;
    public float startTimeBeforeAppear;
    private float timeBeforeAppear;
    public bool canMove;
    public Transform[] targets;
    public bool lerping;
    public float moveSpeed;
    public float nbSecondsAtTarget;
    private float nbSecondsLeftAtTarget;
    private int targetIndex = 0;

    public bool canDamage;
    public int damage; // -1 for insta kill
    public float startTimeBtwDamages;
    private float timeBtwDamages;

    private float timeLeft_WrongSpawnPosAnimation = 0.7f;
    private float startTime_WrongSpawnPosAnimation = 0.7f;
    

    public Rigidbody2D rb;
    private LifeManager lifeManager;
    private Collider2D thisCollider;
    private float gameTimeStamp;

    private bool isCollisionStay;

    // Start is called before the first frame update
    void Start()
    {
        timeBeforeDisap = startTimeBeforeDisap;
        nbSecondsLeftAtTarget = nbSecondsAtTarget;
        timeBeforeAppear = startTimeBeforeAppear;
        thisCollider = gameObject.GetComponent<Collider2D>();
        lifeManager = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeManager>();
    }

    private void FixedUpdate()
    {
  //     if (playerController.gameObject.transform.GetChild(1).position.y > rb.position.y + 0.5f * thisCollider.bounds.size.y + thisCollider.offset.y )
  //     {
  //         Debug.Log("player is above");
  //         Debug.Log(transform.lossyScale.y);
  //
  //         thisCollider.isTrigger = false;
  //     }
  //     else
  //     {
  //         thisCollider.isTrigger = true;
  //     }


        
        if (canMove && !PauseManager.IsPaused())
        {
            Move();
        }


        if (canDisappear)
        {
            if (!disappearPermanently && !gameObject.GetComponent<Collider2D>().enabled)
            {


                if (timeBeforeAppear <= 0f)
                {
                    gameObject.GetComponent<Collider2D>().enabled = true;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    timeBeforeAppear = startTimeBeforeAppear;
                }
                else
                {
                    timeBeforeAppear -= Time.deltaTime;
                }
            }
        }



    }

    public void Move()
    {

        if (Vector2.Distance(this.transform.position, targets[targetIndex].position) <= 2f)
        {
            if (nbSecondsLeftAtTarget <= 0)
            {
                if (targetIndex == targets.Length - 1)
                {
                    targetIndex = 0;
                }
                else
                {
                    targetIndex++;
                }
                nbSecondsLeftAtTarget = nbSecondsAtTarget;
            }
            else
            {
                nbSecondsLeftAtTarget -= Time.deltaTime;
            }
        }
        else
        {
            if (lerping)
            {
                transform.position = Vector2.Lerp(transform.position, targets[targetIndex].position, moveSpeed * Time.deltaTime);
            }
            else
            {
                Vector3 dir = new Vector2(targets[targetIndex].position.x - transform.position.x, targets[targetIndex].position.y - transform.position.y).normalized;
                rb.MovePosition(transform.position + dir * Time.fixedDeltaTime * moveSpeed);
            }
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PauseManager.IsPaused())
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (canDamage)
                {
                    if (damage == -1)
                    {
                        lifeManager.Lives = 0;
                    }
                    else
                    {
                        lifeManager.Lives -= damage;
                    }

                    timeBtwDamages = startTimeBtwDamages;
                }
            }
        }
        if (collision.otherCollider.CompareTag("PlayerBlock"))
        {
            targets[targetIndex].position = transform.position;

            
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider.CompareTag("Player"))
        {
            if (canDisappear)
            {
                if (timeBeforeDisap != 0 && gameObject.GetComponent<Collider2D>().enabled)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                }
            }
        }
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            if (canDisappear)
            {
                if (timeBeforeDisap <= 0)
                {
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.05f);
                    timeBeforeDisap = startTimeBeforeDisap;
                }
                else
                {

                    WillDisappearPlayAnim();

                    timeBeforeDisap -= Time.deltaTime;
                }
            }

            if (canDamage)
            {
                if (gameTimeStamp != Time.fixedDeltaTime)
                {
                    timeBtwDamages = startTimeBtwDamages;
                }
                if (timeBtwDamages <= 0f)
                {
                    if (damage == -1)
                    {
                        lifeManager.Lives = 0;
                    }
                    else
                    {
                        lifeManager.Lives -= damage;
                    }
                    timeBtwDamages = startTimeBtwDamages;
                }
                else
                {
                    timeBtwDamages -= Time.deltaTime;
                    gameTimeStamp = Time.fixedDeltaTime;
                }

            }
        }

        void WillDisappearPlayAnim()
        {
            float animateSpeed = 7f;
            
            SpriteRenderer thisSprite = gameObject.GetComponent<SpriteRenderer>();
            if (timeLeft_WrongSpawnPosAnimation >= 0.5f * startTime_WrongSpawnPosAnimation)
            {
                thisSprite.color = Color.Lerp(thisSprite.color, new Color(thisSprite.color.r, thisSprite.color.g, thisSprite.color.b, 0.2f), Time.deltaTime * animateSpeed);
                timeLeft_WrongSpawnPosAnimation -= Time.deltaTime;
            }
            else if (timeLeft_WrongSpawnPosAnimation <= 0.5f * startTime_WrongSpawnPosAnimation && timeLeft_WrongSpawnPosAnimation >= 0f)
            {
                thisSprite.color = Color.Lerp(thisSprite.color, new Color(1f, 1f, 1f, 1f), Time.deltaTime * animateSpeed);
                timeLeft_WrongSpawnPosAnimation -= Time.deltaTime;
            }
            else if (timeLeft_WrongSpawnPosAnimation <= 0f)
            {
                timeLeft_WrongSpawnPosAnimation = startTime_WrongSpawnPosAnimation;

            }
        }
    }

    



    
}




// block behaviors: 
// - move (only smoothdamp)
// - prevent collision with placed block
// - collide with placed blocks
//   - disappear with anim
//   - explode and disappear
//   - disappear and return to initial position
//
//
// Zone behavior
// - Movement
//   - stationary
//   - bounce
//   - waypoints
//   - random shape, etc.
// - Event trigger (use color code or signs?)
//   - falling block
//   - moving block
//   - rotate block
//   - open new area
//   - 
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
