public class PlayerJumpState : PlayerAbilityState {
    private int _jumpCounter;
    public bool CanJump => _jumpCounter < owner.Data.amountOfJumps;

    public PlayerJumpState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();

        owner.SetVelocityY(owner.Data.jumpVelocity);
        IncreaseJumpCounter();
        isAbilityDone = true;
    }

    public void ResetJumpCounter() => _jumpCounter = 0;

    public void IncreaseJumpCounter() => _jumpCounter++;
}