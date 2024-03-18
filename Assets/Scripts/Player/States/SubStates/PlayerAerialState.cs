using UnityEngine;

public class PlayerAerialState : PlayerState
{
    private PlayerJumpState _jumpState;
    private PlayerDashState _dashState;

    private bool _jumpInput;
    private bool _grabInput;
    private bool _dashInput;
    private bool[] _attackInputs;

    private float _coyoteTimer;
    private bool InCoyoteTime => _coyoteTimer > 0;

    private bool _isJumping;

    public PlayerAerialState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(
        stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _coyoteTimer = owner.StatesConfigSo.coyoteTime;
        _jumpState = stateMachine.GetStateInstance<PlayerJumpState>();
        _dashState = stateMachine.GetStateInstance<PlayerDashState>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExiting) return;

        _jumpInput = owner.InputHandler.JumpInput;
        _dashInput = owner.InputHandler.DashInput;
        _grabInput = owner.InputHandler.GrabInput;
        _attackInputs = owner.InputHandler.AttackInputs;

        // 主攻击
        if (_attackInputs[(int)CombatInputs.Primary] && owner.WeaponsHolder.IsPrimaryWeaponExists)
        {
            stateMachine.TransitionTo<PlayerPrimaryAttackState>();
        }
        // 副攻击
        else if (_attackInputs[(int)CombatInputs.Secondary] && owner.WeaponsHolder.IsSecondaryWeaponExists)
        {
            stateMachine.TransitionTo<PlayerSecondaryAttackState>();
        }
        
        // 接触地面且纵向速度向下时，进入落地状态
        if (core.CollisionSenses.IsGrounded && core.Movement.CurrentVelocity.y <= 0f)
        {
            stateMachine.TransitionTo<PlayerLandState>();
        }
        // 跳墙
        else if (_jumpInput && core.CollisionSenses.IsTouchingWall)
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
        else if (_dashInput && _dashState.CanDash)
            // 冲刺
        {
            stateMachine.TransitionTo<PlayerDashState>();
        }
        else if (core.CollisionSenses.IsTouchingWall)
        {
            // 抓墙
            if (_grabInput && core.CollisionSenses.IsTouchingLedge)
            {
                stateMachine.TransitionTo<PlayerWallGrabState>();
            }
            // 平台攀爬
            else if (!core.CollisionSenses.IsTouchingLedge)
            {
                stateMachine.TransitionTo<PlayerLedgeClimbState>();
            }
            // 横向输入与面朝向一致且速度向下时，进入滑墙状态
            else if (core.Movement.FacingDirection == InputX && core.Movement.CurrentVelocity.y <= 0f)
            {
                stateMachine.TransitionTo<PlayerWallSlideState>();
            }
        }
        else
        {
            core.Movement.SetVelocityX(owner.StatesConfigSo.movementVelocity *
                                       owner.StatesConfigSo.airSteeringMultiplier * InputX);
            core.Movement.CheckIfShouldFlip(InputX);

            // 更新Animator变量
            owner.Animator.SetFloat(velocityXHash, Mathf.Abs(core.Movement.CurrentVelocity.x));
            owner.Animator.SetFloat(velocityYHash, core.Movement.CurrentVelocity.y);

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
            core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y *
                                       owner.StatesConfigSo.variableJumpHeightMultiplier);
            _isJumping = false;
        }
        // 人物速度向下时，设置_isJumping为false
        else if (core.Movement.CurrentVelocity.y <= 0)
        {
            _isJumping = false;
        }
    }

    // 从跳跃状态进入此状态时，应调用该函数
    public void SetIsJumping() => _isJumping = true;
}