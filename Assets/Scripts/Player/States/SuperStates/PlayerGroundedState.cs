using UnityEngine;

public class PlayerGroundedState : PlayerState {
    private PlayerJumpState _jumpState;
    protected int InputX => owner.InputHandler.NormalizedMovementInput.x;
    private bool GrabInput => owner.InputHandler.GrabInput;

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

        if (owner.InputHandler.JumpInput && _jumpState.CanJump)
        {
            stateMachine.TransitionTo<PlayerJumpState>();
        }
        else if (GrabInput && owner.IsTouchingWall)
        {
            stateMachine.TransitionTo<PlayerWallGrabState>();
        }
        else if (!owner.IsGrounded)
        {
            stateMachine.TransitionTo<PlayerAerialState>();
        }
    }
}