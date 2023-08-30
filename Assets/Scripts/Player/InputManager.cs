using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Pausable))]
[RequireComponent(typeof(Jump))]
public class InputManager : MonoBehaviour
{
    private PlayerInput _inputs;
    private string _prevActionMap = "Default";

    private float _horizontal = 0.0f;
    private float _vertical = 0.0f;

    [SerializeField] private EditingController _editingController;
    private HorizontalMove _walkBehaviour;
    private Jump _jumpBehaviour;
    private Dash _dashBehaviour;
    private WallSlide _wallSlideBehaviour;

    void Awake()
    {
        _inputs = GetComponent<PlayerInput>();
        _jumpBehaviour = GetComponent<Jump>();
    }

    void Update()
    {
        if (!PauseManager.IsPaused() && !_editingController.IsEditing())
        {
            _walkBehaviour.MoveHorizontal(new Vector2(_horizontal, 0f));
            _wallSlideBehaviour.SetHorizontalInput(_horizontal);
        };
    }

    // Default mode
    void OnHorizontal(InputValue value)
    {
        _horizontal = value.Get<float>();
    }

    void OnVertical(InputValue value)
    {
        _vertical = value.Get<float>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            _jumpBehaviour.BeginJump();
        }
        else
        {
            _jumpBehaviour.EndJump();
        }
    }

    void OnDash()
    {
        _dashBehaviour.InitiateDash(new Vector2(_horizontal, _vertical));
    }

    void OnSprint(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Starting sprint is not enabled yet!");
        }
        else
        {
            Debug.Log("Stopping sprint is not enabled yet!");
        }
    }

    void OnCrouch(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Starting crouch is not enabled yet!");
        }
        else
        {
            Debug.Log("Stopping crouch is not enabled yet!");
        }
    }

    void OnEnterEditMode()
    {
        if (_editingController.BeginEditing())
            _inputs.SwitchCurrentActionMap("Editing");
    }

    // Edit mode
    void OnSelectNext()
    {
        Debug.Log("Selecting next is not enabled yet!");
    }

    void OnSelectPrev()
    {
        Debug.Log("Selecting next is not enabled yet!");
    }

    void OnSelectPlaceable(InputValue value)
    {
        // TODO: use a variable float with sum
        Debug.Log("Selecting with " + value.Get<float>() + " is not enabled yet!");
    }

    void OnRotateCW()
    {
        Debug.Log("Rotating clockwise is not enabled yet!");
    }

    void OnRotateCCW()
    {
        Debug.Log("Rotating counter-clockwise is not enabled yet!");
    }

    void OnPlace()
    {
        Debug.Log("Placing is not enabled yet!");
    }

    void OnErase(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Activating erase mode is not enabled yet!");
        }
        else
        {
            Debug.Log("Deactivating erase mode is not enabled yet!");
        }
    }

    void OnExitEditMode()
    {
        _editingController.StopEditing();
        _inputs.SwitchCurrentActionMap("Default");
    }

    // Common to default and edit
    void OnTriggerPause()
    {
        PauseManager.Pause();
    }

    // Pause menu specific
    public void OnTriggerResume()
    {
        PauseManager.Resume();
    }

    // Handling pause signals
    void OnPause()
    {
        _prevActionMap = _inputs.currentActionMap.name;
        _inputs.SwitchCurrentActionMap("PauseMenu");
    }

    void OnResume()
    {
        _inputs.SwitchCurrentActionMap(_prevActionMap);
    }
}
