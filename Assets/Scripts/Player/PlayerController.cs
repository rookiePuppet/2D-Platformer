using UnityEngine;

public class PlayerController : MonoBehaviour {
    #region Components

    public PlayerStateMachine StateMachine { get; private set; }

    [SerializeField] private PlayerData playerData;
    public PlayerData Data => playerData;
    private Rigidbody2D Rigidbody { get; set; }
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    #endregion

    #region Check Variables

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    #endregion

    #region Character Properties

    public bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.groundLayer);
    public bool IsTouchingWall => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.groundLayer);
    public int FacingDirection { get; private set; } = 1;
    public Vector2 CurrentVelocity => Rigidbody.velocity;

    #endregion

    #region Other Variables

    private Vector2 _velocity;

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Start()
    {
        StateMachine = new PlayerStateMachine(this);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);
        var position = wallCheck.position;
        Gizmos.DrawLine(position, position + Vector3.right * FacingDirection * playerData.wallCheckDistance);
    }

    #endregion

    #region Set Functions

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _velocity.x = angle.x * direction * velocity;
        _velocity.y = angle.y * velocity;
        Rigidbody.velocity = _velocity;
    }

    public void SetVelocity(float x, float y)
    {
        _velocity.x = x;
        _velocity.y = y;

        Rigidbody.velocity = _velocity;
    }

    public void SetVelocityX(float velocityX) => SetVelocity(velocityX, CurrentVelocity.y);

    public void SetVelocityY(float velocityY) => SetVelocity(CurrentVelocity.x, velocityY);

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void ClimbAction()
    {
        SetPosition(groundCheck.position + Vector3.up * 1.5f + Vector3.right * 1.5f);
    }

    #endregion

    #region Check Functions

    public void CheckIfShouldFlip(int inputX)
    {
        if (inputX != 0 && inputX != FacingDirection)
        {
            Flip();
        }

        return;

        void Flip()
        {
            FacingDirection *= -1;
            transform.Rotate(Vector3.up, 180F);
        }
    }

    #endregion
}