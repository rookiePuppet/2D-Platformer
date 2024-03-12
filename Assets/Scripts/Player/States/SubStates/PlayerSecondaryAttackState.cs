public class PlayerSecondaryAttackState : PlayerAttackState
{
    public PlayerSecondaryAttackState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void SetWeapon(Weapon weapon)
    {
        base.SetWeapon(weapon);
        
        weapon.InitializeWeapon(this);
    }
}