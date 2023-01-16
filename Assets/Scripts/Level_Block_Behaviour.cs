using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Block_Behaviour : MonoBehaviour
{

    public bool canDisappear;
    public float startTimeBeforeDisap;
    private float timeBeforeDisap;

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
    private playerController playerController;
    private float gameTimeStamp;

    // Start is called before the first frame update
    void Start()
    {
        timeBeforeDisap = startTimeBeforeDisap;
        nbSecondsLeftAtTarget = nbSecondsAtTarget;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }

    private void FixedUpdate()
    {
        if (canMove && !PauseMenu.isPaused)
        {
            Move();
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
        if (PauseMenu.isPaused)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (canDamage)
                {
                    if (damage == -1)
                    {
                        playerController.Lives = 0;
                    }
                    else
                    {
                        playerController.Lives -= damage;
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

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            if (canDisappear)
            {
                if (timeBeforeDisap <= 0)
                {
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.05f);
                }
                else
                {

                    PlayAnim();

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
                        playerController.Lives = 0;
                    }
                    else
                    {
                        playerController.Lives -= damage;
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

        void PlayAnim()
        {
            float animateSpeed = 7f;
            if (timeBeforeDisap <= 0)
            {
                gameObject.GetComponent<Collider2D>().enabled = true;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
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
