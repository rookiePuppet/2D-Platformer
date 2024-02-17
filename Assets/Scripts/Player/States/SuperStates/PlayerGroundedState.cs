using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private PlayerJumpState _jumpState;

    private bool _jumpInput;
    private bool _grabInput;
    private bool _dashInput;
    private bool[] _attackInputs;

    protected PlayerGroundedState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _jumpState = stateMachine.GetStateInstance<PlayerJumpState>();
        _jumpState.ResetJumpCounter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _jumpInput = owner.InputHandler.JumpInput;
        _grabInput = owner.InputHandler.GrabInput;
        _dashInput = owner.InputHandler.DashInput;
        _attackInputs = owner.InputHandler.AttackInputs;

        // 主攻击
        if (_attackInputs[(int)CombatInputs.Primary])
        {
            stateMachine.TransitionTo<PlayerPrimaryAttackState>();
        }
        // 副攻击
        else if (_attackInputs[(int)CombatInputs.Secondary])
        {
            stateMachine.TransitionTo<PlayerSecondaryAttackState>();
        }
        // 跳跃
        else if (_jumpInput && _jumpState.CanJump)
        {
            stateMachine.TransitionTo<PlayerJumpState>();
        }
        else if (_grabInput && core.CollisionSenses.IsTouchingWall)
        {
            stateMachine.TransitionTo<PlayerWallGrabState>();
        }
        else if (_dashInput && !core.CollisionSenses.IsTouchingCeiling)
        {
            stateMachine.TransitionTo<PlayerDashState>();
        }
        else if (!core.CollisionSenses.IsGrounded)
        {
            stateMachine.TransitionTo<PlayerAerialState>();
        }
    }
}