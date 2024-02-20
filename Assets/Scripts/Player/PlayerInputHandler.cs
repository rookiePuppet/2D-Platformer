using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private float inputHoldTime = 0.2f;

    public Vector2 RawMoveInput { get; set; }
    public Vector2Int NormalizedMoveInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }

    public Vector2 mousePosition;

    public bool JumpInput
    {
        get
        {
            var input = _jumpInput;
            _jumpInput = false;
            return input;
        }
        private set => _jumpInput = value;
    }
    public bool JumpInputStop { get; private set; }
    public bool DashInput
    {
        get
        {
            var input = _dashInput;
            _dashInput = false;
            return input;
        }
        private set => _dashInput = value;
    }
    public bool DashInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool[] AttackInputs { get; private set; }
    public bool InteractInput
    {
        get
        {
            var input = _interactInput;
            _interactInput = false;
            return input;
        }
        private set => _interactInput = value;
    }

    private bool _jumpInput;
    private float _jumpInputStartTime;
    private bool _dashInput;
    private float _dashInputStartTime;
    private bool _interactInput;
    private PlayerInput _playerInput;

    private Camera _mainCamera;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        AttackInputs = new bool[Enum.GetValues(typeof(CombatInputs)).Length];
    }

    private void Update()
    {
        CheckInputHoldTime();
    }
    
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractInput = true;
        }
        else if (context.canceled)
        {
            InteractInput = false;
        }
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.Primary] = true;
        }
        else if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.Primary] = false;
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.Secondary] = true;
        }
        else if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.Secondary] = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMoveInput = context.ReadValue<Vector2>();
        var x = Mathf.RoundToInt(RawMoveInput.x);
        var y = Mathf.RoundToInt(RawMoveInput.y);
        NormalizedMoveInput = new Vector2Int(x, y);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            _jumpInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }
        else if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            _dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        // 键鼠操作，方向向量从角色位置指向鼠标位置
        if (_playerInput.currentControlScheme == "Keyboard")
        {
            RawDashDirectionInput = (RawDashDirectionInput - (Vector2)_mainCamera.WorldToScreenPoint(transform.position)).normalized;
        }
    }

    private void CheckInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }

        if (Time.time >= _dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
}

public enum CombatInputs
{
    Primary,
    Secondary
}