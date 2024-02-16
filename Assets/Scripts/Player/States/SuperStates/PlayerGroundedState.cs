using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private PlayerJumpState _jumpState;

    private bool _jumpInput;
    private bool _grabInput;
    private bool _dashInput;

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

        if (_jumpInput && _jumpState.CanJump)
        {
            stateMachine.TransitionTo<PlayerJumpState>();
        }
        else if (_grabInput && owner.IsTouchingWall)
        {
            stateMachine.TransitionTo<PlayerWallGrabState>();
        }
        else if (_dashInput && !owner.IsTouchingCeiling)
        {
            stateMachine.TransitionTo<PlayerDashState>();
        }
        else if (!owner.IsGrounded)
        {
            stateMachine.TransitionTo<PlayerAerialState>();
        }
    }
}