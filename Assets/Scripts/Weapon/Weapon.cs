using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract WeaponDataSO WeaponData { get;}
    
    protected Animator baseAnimator;
    protected Animator weaponAnimator;

    protected PlayerController owner;
    protected PlayerAttackState state;

    protected CombatInputs WeaponOrder;

    protected virtual void Awake()
    {
        owner = GetComponentInParent<PlayerController>();
        
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