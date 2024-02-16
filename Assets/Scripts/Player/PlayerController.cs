using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components

    public PlayerStateMachine StateMachine { get; private set; }

    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform dashDirectionIndicator;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform ceilingCheck;

    public PlayerData Data => playerData;
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Transform DashDirectionIndicator => dashDirectionIndicator;
    private BoxCollider2D _collider;

    #endregion

    #region Character Properties

    public bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.groundLayer);

    public bool IsTouchingWall => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection,
        playerData.wallCheckDistance, playerData.groundLayer);

    public bool IsTouchingLedge => Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection,
        playerData.wallCheckDistance, playerData.groundLayer);

    public bool IsTouchingCeiling => Physics2D.OverlapCircle(ceilingCheck.position, Data.groundCheckRadius,
        playerData.groundLayer);

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
        _collider = GetComponent<BoxCollider2D>();
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
        Gizmos.DrawLine(ceilingCheck.position, ceilingCheck.position + Vector3.up * playerData.ceilingCheckDistance);
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

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _velocity.x = direction.x * velocity;
        _velocity.y = direction.y * velocity;
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

    public void SetCrouchCollider()
    {
        var temp = _collider.size;
        temp.y = playerData.crouchColliderHeight;
        _collider.size = temp;

        temp = _collider.offset;
        temp.y = playerData.normalColliderYOffset - (playerData.normalColliderHeight - playerData.crouchColliderHeight) / 2;
        _collider.offset = temp;
    }

    public void SetNormalCollider()
    {
        var temp = _collider.size;
        temp.y = playerData.normalColliderHeight;
        _collider.size = temp;

        temp = _collider.offset;
        temp.y = playerData.normalColliderYOffset;
        _collider.offset = temp;
    }
}