using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour {
    public Vector2 RawMovementInput { get; set; }
    public Vector2Int NormalizedMovementInput { get; private set; }
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
    public bool GrabInput { get; private set; }
    private bool _jumpInput;
    private float _jumpInputStartTime;
    public bool JumpInputStop { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;

    private PlayerInputActions _playerInput;

    #region Unity Callback Functions

    private void Awake()
    {
        _playerInput = new PlayerInputActions();
        _playerInput.Gameplay.Enable();
    }

    private void Update()
    {
        CheckMovementInput();
        CheckInputHoldTime();
    }

    private void OnEnable()
    {
        _playerInput.Gameplay.Jump.started += OnJumpStarted;
        _playerInput.Gameplay.Jump.canceled += OnJumpCanceled;
        _playerInput.Gameplay.Grab.started += OnGrabStarted;
        _playerInput.Gameplay.Grab.canceled += OnGrabCanceled;
    }

    private void OnDisable()
    {
        _playerInput.Gameplay.Jump.started -= OnJumpStarted;
        _playerInput.Gameplay.Jump.canceled -= OnJumpCanceled;
        _playerInput.Gameplay.Grab.started -= OnGrabStarted;
        _playerInput.Gameplay.Grab.canceled -= OnGrabCanceled;
    }

    #endregion

    private void CheckMovementInput()
    {
        RawMovementInput = _playerInput.Gameplay.Movement.ReadValue<Vector2>();
        
        var x = Mathf.Abs(RawMovementInput.x) > 0.5f ? (int)(RawMovementInput.x * Vector2.right).normalized.x : 0;
        var y = Mathf.Abs(RawMovementInput.y) > 0.5f ? (int)(RawMovementInput.y * Vector2.up).normalized.y : 0;
        NormalizedMovementInput = new Vector2Int(x, y);
    }

    private void OnJumpStarted(InputAction.CallbackContext obj)
    {
        JumpInput = true;
        JumpInputStop = false;
        _jumpInputStartTime = Time.time;
    }

    private void OnJumpCanceled(InputAction.CallbackContext obj) => JumpInputStop = true;

    private void OnGrabStarted(InputAction.CallbackContext obj) => GrabInput = true;

    private void OnGrabCanceled(InputAction.CallbackContext obj) => GrabInput = false;

    private void CheckInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}