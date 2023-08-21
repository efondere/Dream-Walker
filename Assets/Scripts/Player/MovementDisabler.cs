using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDisabler : MonoBehaviour
{
    HorizontalMove horizontalMove;
    Jump jump;
    WallSlide wallSlide;
    Dash dash;
    InputManager inputManager;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        horizontalMove = GameObject.FindWithTag("Player").GetComponent<HorizontalMove>();
        jump = GameObject.FindWithTag("Player").GetComponent<Jump>();
        dash = GameObject.FindWithTag("Player").GetComponent<Dash>();
        wallSlide = GameObject.FindWithTag("Player").GetComponent<WallSlide>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.IsPaused())
        {
            horizontalMove.enabled = jump.enabled = dash.enabled = wallSlide.enabled = false;
        }
        else if (inputManager.IsEditing())
        {
            horizontalMove.enabled = jump.enabled = dash.enabled = wallSlide.enabled = false;
        }
        else
        {
            horizontalMove.enabled = jump.enabled = dash.enabled = wallSlide.enabled = true;
        }

    }
}
