using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    [SerializeField] private Transform ledgeCheck;

    #endregion

    #region Character Properties

    public bool IsGrounded =>
        Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.groundLayer);

    public bool IsTouchingWall => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection,
        playerData.wallCheckDistance, playerData.groundLayer);

    public bool IsTouchingLedge => Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection,
        playerData.wallCheckDistance, playerData.groundLayer);

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
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * FacingDirection * playerData.wallCheckDistance);
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + Vector3.right * FacingDirection * playerData.wallCheckDistance);
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

    public Vector2 DetermineCornerPosition()
    {
        var wallCheckPos = wallCheck.position;
        var ledgeCheckPos = ledgeCheck.position;
        var direction = Vector2.right * FacingDirection;

        // 获得角色与墙壁的横向距离
        var wallHit = Physics2D.Raycast(wallCheckPos, direction, playerData.wallCheckDistance, playerData.groundLayer);
        var xDis = wallHit.distance;

        // 获得头顶到地面的垂直距离
        var groundHit = Physics2D.Raycast((Vector2)ledgeCheckPos + direction * xDis,
            Vector2.down, ledgeCheckPos.y - wallCheckPos.y, playerData.groundLayer);
        var yDis = groundHit.distance;

        return new Vector2(wallCheckPos.x + xDis * FacingDirection, ledgeCheckPos.y - yDis);
    }
}