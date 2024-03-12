public class PlayerAttackState : PlayerAbilityState
{
    private Weapon _weapon;
    private float _velocityToSet;
    private bool _setVelocity;
    private bool _shouldCheckFlip;

    protected PlayerAttackState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) :
        base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _setVelocity = false;
        _weapon.EnterWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_setVelocity)
        {
            core.Movement.SetVelocityX(_velocityToSet * core.Movement.FacingDirection);
        }

        if (_shouldCheckFlip)
        {
            core.Movement.CheckIfShouldFlip(InputX);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _weapon.ExitWeapon();
    }

    public virtual void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }

    public void SetPlayerVelocity(float velocity)
    {
        core.Movement.SetVelocityX(velocity * core.Movement.FacingDirection);

        _velocityToSet = velocity;
        _setVelocity = true;
    }

    public void SetShouldCheckFlip(bool value)
    {
        _shouldCheckFlip = value;
    }

    public bool CombatInput(CombatInputs type) => owner.InputHandler.AttackInputs[(int)type];
}