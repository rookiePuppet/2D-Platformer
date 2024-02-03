using UnityEngine;

public class PlayerAerialState : PlayerState {
    private bool GrabInput => owner.InputHandler.GrabInput;

    private PlayerJumpState _jumpState;

    private bool _jumpInput;
    private float _coyoteTimer;
    private bool InCoyoteTime => _coyoteTimer > 0;

    private bool _isJumping;

    public PlayerAerialState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _coyoteTimer = owner.Data.coyoteTime;
        _jumpState = stateMachine.GetStateInstance<PlayerJumpState>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExiting) return;

        _jumpInput = owner.InputHandler.JumpInput;

        // 接触地面且纵向速度向下时，进入落地状态
        if (owner.IsGrounded && owner.CurrentVelocity.y <= 0f)
        {
            stateMachine.TransitionTo<PlayerLandState>();
        }   
        // 跳墙
        else if (_jumpInput && owner.IsTouchingWall)
        {
            stateMachine.TransitionTo<PlayerWallJumpState>();
        }
        // 空中跳跃
        else if (_jumpInput && _jumpState.CanJump)
        {
            stateMachine.TransitionTo<PlayerJumpState>();
            // 土狼时间过后起跳才消耗一次跳跃次数
            if (!InCoyoteTime) _jumpState.IncreaseJumpCounter();
        }
        else if (owner.IsTouchingWall)
        {
            // 抓墙
            if (GrabInput)
            {
                stateMachine.TransitionTo<PlayerWallGrabState>();
            }
            // 平台攀爬
            else if (!owner.IsTouchingLedge)
            {
                stateMachine.TransitionTo<PlayerLedgeClimbState>();
            }
            // 横向输入与面朝向一致且速度向下时，进入滑墙状态
            else if (owner.FacingDirection == InputX && owner.CurrentVelocity.y <= 0f)
            {
                stateMachine.TransitionTo<PlayerWallSlideState>();
            }
        }
        else
        {
            owner.SetVelocityX(owner.Data.movementVelocity * owner.Data.airSteeringMultiplier * InputX);
            owner.CheckIfShouldFlip(InputX);
            
            // 更新Animator变量
            owner.Animator.SetFloat(velocityXHash, Mathf.Abs(owner.CurrentVelocity.x));
            owner.Animator.SetFloat(velocityYHash, owner.CurrentVelocity.y);

            _coyoteTimer -= Time.deltaTime;

            CheckJumpMultiplier();
        }
    }

    /// <summary>
    /// 根据玩家跳跃键的维持时间改变跳跃高度
    /// </summary>
    private void CheckJumpMultiplier()
    {
        if (!_isJumping) return;
        // 跃起过程中松开跳跃键，会降低跳跃高度
        if (owner.InputHandler.JumpInputStop)
        {
            owner.SetVelocityY(owner.CurrentVelocity.y * owner.Data.variableJumpHeightMultiplier);
            _isJumping = false;
        }
        // 人物速度向下时，设置_isJumping为false
        else if (owner.CurrentVelocity.y <= 0)
        {
            _isJumping = false;
        }
    }

    // 从跳跃状态进入此状态时，应调用该函数
    public void SetIsJumping() => _isJumping = true;
}