﻿public class PlayerJumpState : PlayerAbilityState
{
    private int _jumpCounter;
    public bool CanJump => _jumpCounter < owner.StatesConfigSo.amountOfJumps;

    public PlayerJumpState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(
        stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();

        core.Movement.SetVelocityY(owner.StatesConfigSo.jumpVelocity);
        IncreaseJumpCounter();
        isAbilityDone = true;
    }

    public void ResetJumpCounter() => _jumpCounter = 0;

    public void IncreaseJumpCounter() => _jumpCounter++;

    public void SetJumpCounterWhenWallJump() => _jumpCounter = owner.StatesConfigSo.amountOfJumps;
}