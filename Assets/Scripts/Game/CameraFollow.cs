using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = .5f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset;
    public Camera cam;

    public static bool shouldCameraFollow = false;
    // private float savedMoveSpeed;
    // private bool moveSpeedIsSaved = false;
    // public float distFromCamToStopFollow;
    // public float distFromCamToStartFollow;
    private InputManager inputManager;
    private Transform playerTransform;
    public AudioClip mainClip;
    public AudioSource audioSource;

    public float zoomVelocity;
    public float startTimeBeforeZoomOut;
    private float timeBeforeZoomOut;
    private float playerYSpeed;

    private void Start()
    {
        inputManager = GameObject.FindWithTag("Player").GetComponent<InputManager>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        // camera follow



        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothTime);


        //     if (shouldCameraFollow == true)
        //     {
        //         transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothTime);
        //     }
        //
        //     if (Vector3.Distance(transform.position, target.position + offset) <= distFromCamToStopFollow)
        //     {
        //         shouldCameraFollow = false;
        //     }
        //
        //     if (Vector3.Distance(transform.position, target.position + offset) >= distFromCamToStartFollow)
        //     {
        //         shouldCameraFollow = true;
        //
        //     }

        Debug.Log("Axis Horizontal = " + Input.GetAxis("Horizontal"));

        // camera zooming out (if player is editing, or is moving) or in
        //if (Mathf.Abs(inputManager.HorizontalInput().x) <= 0.01f || Mathf.Abs(inputManager.JumpInput()) < 0 || (inputManager.IsEditing() && timeBeforeZoomOut <= 0.01f))
        if (false)
        {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, 7f, ref zoomVelocity, 1f);
        }
        else if (cam.orthographicSize >= 5.01f)
        {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, 5f, ref zoomVelocity, 0.4f);
            timeBeforeZoomOut = startTimeBeforeZoomOut;
        }
        else if (timeBeforeZoomOut > 0f)
        {
            timeBeforeZoomOut -= Time.deltaTime;
        }
    }
}
