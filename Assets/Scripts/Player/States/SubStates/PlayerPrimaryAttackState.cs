public class PlayerPrimaryAttackState : PlayerAbilityState
{
    private Weapon _weapon;
    private float _velocityToSet;
    private bool _setVelocity;
    private bool _shouldCheckFlip;

    public PlayerPrimaryAttackState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) :
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

    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
        _weapon.InitializeWeapon(this);
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
}