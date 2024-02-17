using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData_SO weaponData;

    protected Animator baseAnimator;
    protected Animator weaponAnimator;
    protected int attackCounter;

    protected static readonly int AttackHash = Animator.StringToHash("Attack");
    protected static readonly int AttackCounterHash = Animator.StringToHash("AttackCounter");

    private PlayerPrimaryAttackState _state;

    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }

    public void InitializeWeapon(PlayerPrimaryAttackState state)
    {
        _state = state;
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);
        baseAnimator.SetBool(AttackHash, true);
        weaponAnimator.SetBool(AttackHash, true);

        attackCounter++;
        if (attackCounter > weaponData.movementSpeed.Length)
        {
            attackCounter = 1;
        }

        baseAnimator.SetInteger(AttackCounterHash, attackCounter);
        weaponAnimator.SetInteger(AttackCounterHash, attackCounter);
    }

    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool(AttackHash, false);
        weaponAnimator.SetBool(AttackHash, false);
        gameObject.SetActive(false);
    }

    public virtual void AnimationFinishTrigger()
    {
        _state.AnimationFinishTrigger();
    }

    /// <summary>
    /// 攻击开始时移动速度设置
    /// </summary>
    public virtual void AnimationStartMovementTrigger()
    {
        _state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter - 1]);
    }

    /// <summary>
    /// 攻击结束时移动速度设置
    /// </summary>
    public virtual void AnimationStopMovementTrigger()
    {
        _state.SetPlayerVelocity(0f);
    }

    public virtual void AnimationStartFlipCheck()
    {
        _state.SetShouldCheckFlip(true);
    }

    public virtual void AnimationStopFlipCheck()
    {
        _state.SetShouldCheckFlip(false);
    }
}
