using UnityEngine;

public class PlayerAerialState : PlayerState {
    private readonly int _velocityXHash = Animator.StringToHash("VelocityX");
    private readonly int _velocityYHash = Animator.StringToHash("VelocityY");
    
    private int InputX => (int)owner.InputHandler.RawMovementInput.x;

    private PlayerJumpState _jumpState;

    private float _coyoteTimer;
    private bool InCoyoteTime => _coyoteTimer > 0;

    private bool _isJumping;

    public PlayerAerialState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _coyoteTimer = owner.PlayerData.coyoteTime;
        _jumpState = stateMachine.GetStateInstance<PlayerJumpState>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        // 接触地面且纵向速度向下时，进入落地状态
        if (owner.IsGrounded && owner.CurrentVelocity.y <= 0)
        {
            stateMachine.TransitionTo<PlayerLandState>();
        }

        // 空中跳跃
        if (owner.InputHandler.JumpInput && _jumpState.CanJump)
        {
            stateMachine.TransitionTo<PlayerJumpState>();
            // 土狼时间过后起跳才消耗一次跳跃次数
            if (!InCoyoteTime) _jumpState.IncreaseJumpCounter();
        }

        if (owner.IsTouchingWall)
        {
            stateMachine.TransitionTo<PlayerWallSlideState>();
        }
        
        // 更新Animator变量
        owner.Animator.SetFloat(_velocityXHash, Mathf.Abs(owner.CurrentVelocity.x));
        owner.Animator.SetFloat(_velocityYHash, owner.CurrentVelocity.y);
        
        _coyoteTimer -= Time.deltaTime;

        CheckJumpMultiplier();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        owner.SetVelocityX(owner.PlayerData.movementVelocity * InputX);
        owner.CheckIfShouldFlip(InputX);
    }

    private void CheckJumpMultiplier()
    {
        if (!_isJumping) return;
        // 跃起过程中松开跳跃键，会降低跳跃高度
        if (owner.InputHandler.JumpInputStop)
        {
            owner.SetVelocityY(owner.CurrentVelocity.y * owner.PlayerData.variableJumpHeightMultiplier);
            _isJumping = false;
        }
        else if (owner.CurrentVelocity.y <= 0)
        {
            _isJumping = false;
        }
    }

    // 从跳跃状态进入此状态时，应调用该函数
    public void SetIsJumping() => _isJumping = true;
}