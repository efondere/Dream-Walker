using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = .5f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset;
    public static bool shouldCameraFollow = false;
   // private float savedMoveSpeed;
   // private bool moveSpeedIsSaved = false;
    public float distFromCamToStopFollow;
    public float distFromCamToStartFollow;


    void Update()
    {

        if (shouldCameraFollow == true)
        {
         //   if (moveSpeedIsSaved == false)
         //   {
          //      SaveMoveSpeed();
         //   }
            transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothTime);
           // controllablesManager.playerMoveSpeed = 0;
        }

        if (Vector3.Distance(transform.position, target.position + offset) <= distFromCamToStopFollow)
        {
            shouldCameraFollow = false;
       //     if (moveSpeedIsSaved)
//{
       //         controllablesManager.playerMoveSpeed = savedMoveSpeed;
       //     }
       //     moveSpeedIsSaved = false;
        }

        //Debug.Log("DISTANCE " + Vector3.Distance(transform.position, target.position + offset));
        if (Vector3.Distance(transform.position, target.position + offset) >= distFromCamToStartFollow)
        {
            shouldCameraFollow = true;

        }

        //Debug.Log("shouldCameraFollow" + shouldCameraFollow);
    }

 //   void SaveMoveSpeed()
  //  {
       // savedMoveSpeed = controllablesManager.playerMoveSpeed;
      //  moveSpeedIsSaved = true;
  //  }

}
