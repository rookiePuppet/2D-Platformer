using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator baseAnimator;
    protected Animator weaponAnimator;
    private PlayerPrimaryAttackState _state;

    protected static readonly int AttackHash = Animator.StringToHash("Attack");

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
        Debug.Log("Weapon Enter");
    }

    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool(AttackHash, false);
        weaponAnimator.SetBool(AttackHash, false);
        gameObject.SetActive(false);
        Debug.Log("Weapon Exit");
    }

    public virtual void AnimationFinishTrigger()
    {
        _state.AnimationFinishTrigger();
    }
}
