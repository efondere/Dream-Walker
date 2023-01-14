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

    public Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        timeBeforeDisap = startTimeBeforeDisap;
        nbSecondsLeftAtTarget = nbSecondsAtTarget;
    }

    private void FixedUpdate()
    {
        if (canMove)
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

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            if (canDisappear)
            {
                if (timeBeforeDisap <= 0)
                {
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }
                else
                {
                    timeBeforeDisap -= Time.deltaTime;
                }
        }
        }
    }

    

    
}
