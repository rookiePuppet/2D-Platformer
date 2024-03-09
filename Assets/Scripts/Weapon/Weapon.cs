using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO weaponData;

    private Animator _baseAnimator;
    private Animator _weaponAnimator;
    protected int attackCounter;

    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int AttackCounterHash = Animator.StringToHash("AttackCounter");

    private PlayerAttackState _state;

    protected virtual void Awake()
    {
        _baseAnimator = transform.Find("Base").GetComponent<Animator>();
        _weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }

    public void InitializeWeapon(PlayerAttackState state)
    {
        _state = state;
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);
        _baseAnimator.SetBool(AttackHash, true);
        _weaponAnimator.SetBool(AttackHash, true);

        attackCounter++;
        if (attackCounter > weaponData.amountOfAttacks)
        {
            attackCounter = 1;
        }

        _baseAnimator.SetInteger(AttackCounterHash, attackCounter);
        _weaponAnimator.SetInteger(AttackCounterHash, attackCounter);
    }

    public virtual void ExitWeapon()
    {
        _baseAnimator.SetBool(AttackHash, false);
        _weaponAnimator.SetBool(AttackHash, false);
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

    public virtual void AnimationActionTrigger()
    {
    }
}