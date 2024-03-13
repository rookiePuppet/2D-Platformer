using UnityEngine;

public class DefensiveWeapon : Weapon
{
    [SerializeField] private DefensiveWeaponDataSO weaponData;
    private bool Input => state.CombatInput(WeaponOrder);

    private bool _isPerformed; // 是否已经加过减伤率

    private static readonly int DefendHash = Animator.StringToHash("Defend");

    private void Update()
    {
        baseAnimator.SetBool(DefendHash, Input);
        weaponAnimator.SetBool(DefendHash, Input);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        if (_isPerformed)
        {
            owner.PlayerStats.DamageReductionRate -= weaponData.damageReductionRate;
        }

        _isPerformed = false;
        print("Reset DamageReductionRate");
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        
        owner.PlayerStats.DamageReductionRate += weaponData.damageReductionRate;
        _isPerformed = true;
        print(owner.PlayerStats.DamageReductionRate );
    }
}