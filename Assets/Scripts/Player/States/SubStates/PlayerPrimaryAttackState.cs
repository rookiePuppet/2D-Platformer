using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerAbilityState
{
    private int _attackCounter;
    private Weapon _weapon;

    private static readonly int AttackCounterHash = Animator.StringToHash("AttackCounter");

    public PlayerPrimaryAttackState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _weapon.EnterWeapon();

        ResetAttackCounter();
        IncreaseAttackCounter();
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

    private void ResetAttackCounter() {
        _attackCounter = 0;
    }

    private void IncreaseAttackCounter()
    {
        _attackCounter++;
        if (_attackCounter > 3)
        {
            _attackCounter = 1;
        }

        foreach(var anim in _weapon.GetComponentsInChildren<Animator>()) {
            anim.SetInteger(AttackCounterHash, _attackCounter);
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
}