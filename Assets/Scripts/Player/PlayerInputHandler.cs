using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour {
    public Vector2 RawMovementInput { get; private set; }
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
    }

    private void OnDisable()
    {
        _playerInput.Gameplay.Jump.started -= OnJumpStarted;
        _playerInput.Gameplay.Jump.canceled -= OnJumpCanceled;
    }

    #endregion

    private void CheckMovementInput()
    {
        RawMovementInput = _playerInput.Gameplay.Movement.ReadValue<Vector2>();
    }

    private void OnJumpStarted(InputAction.CallbackContext obj)
    {
        JumpInput = true;
        JumpInputStop = false;
        _jumpInputStartTime = Time.time;
    }

    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
        JumpInputStop = true;
    }

    private void CheckInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}