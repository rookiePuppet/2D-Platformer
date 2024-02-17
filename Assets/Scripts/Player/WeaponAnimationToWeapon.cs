using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapon _weapon;

    private void Awake()
    {
        _weapon = GetComponentInParent<Weapon>();
    }

    public void AnimationFinishTrigger()
    {
        _weapon.AnimationFinishTrigger();
    }

    public void AnimationStartMovementTrigger()
    {
        _weapon.AnimationStartMovementTrigger();
    }

    public void AnimationStopMovementTrigger()
    {
        _weapon.AnimationStopMovementTrigger();
    }

    public void AnimationStartFlipCheck()
    {
        _weapon.AnimationStartFlipCheck();
    }

    public void AnimationStopFlipCheck()
    {
        _weapon.AnimationStopFlipCheck();
    }
}
