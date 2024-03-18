using UnityEngine;

public class AggressiveWeapon : Weapon
{
    [SerializeField] protected AggressiveWeaponDataSO aggressiveWeaponData;
    public override WeaponDataSO WeaponData => aggressiveWeaponData;

    protected int attackCounter;

    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int AttackCounterHash = Animator.StringToHash("AttackCounter");
    
    public override void EnterWeapon()
    {
        base.EnterWeapon();
        baseAnimator.SetBool(AttackHash, true);
        weaponAnimator.SetBool(AttackHash, true);

        attackCounter++;
        if (attackCounter > aggressiveWeaponData.amountOfAttacks)
        {
            attackCounter = 1;
        }

        baseAnimator.SetInteger(AttackCounterHash, attackCounter);
        weaponAnimator.SetInteger(AttackCounterHash, attackCounter);
    }

    public override void ExitWeapon()
    {
        base.ExitWeapon();
        baseAnimator.SetBool(AttackHash, false);
        weaponAnimator.SetBool(AttackHash, false);
    }

    public override void AnimationStartMovementTrigger()
    {
        base.AnimationStartMovementTrigger();
        state.SetPlayerVelocity(aggressiveWeaponData.movementSpeed[attackCounter - 1]);
    }

    public override void AnimationStopMovementTrigger()
    {
        base.AnimationStopMovementTrigger();
        state.SetPlayerVelocity(0f);
    }
}