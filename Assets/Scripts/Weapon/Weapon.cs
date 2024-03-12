using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO weaponData;

    protected Animator baseAnimator;
    protected Animator weaponAnimator;

    protected PlayerAttackState state;

    protected CombatInputs WeaponOrder;

    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }

    public void InitializeWeapon(PlayerAttackState state)
    {
        this.state = state;
        WeaponOrder = state is PlayerPrimaryAttackState ? CombatInputs.Primary : CombatInputs.Secondary;
        print(WeaponOrder);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);
    }

    public virtual void ExitWeapon()
    {
        gameObject.SetActive(false);
    }

    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    /// <summary>
    /// 攻击开始时移动速度设置
    /// </summary>
    public virtual void AnimationStartMovementTrigger()
    {
    }

    /// <summary>
    /// 攻击结束时移动速度设置
    /// </summary>
    public virtual void AnimationStopMovementTrigger()
    {
    }

    public virtual void AnimationStartFlipCheck()
    {
        state.SetShouldCheckFlip(true);
    }

    public virtual void AnimationStopFlipCheck()
    {
        state.SetShouldCheckFlip(false);
    }

    public virtual void AnimationActionTrigger()
    {
    }
}