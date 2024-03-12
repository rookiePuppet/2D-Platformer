using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveWeapon : Weapon
{
    private static readonly int DefendHash = Animator.StringToHash("Defend");
    private bool Input => state.CombatInput(WeaponOrder);

    private void Update()
    {
        baseAnimator.SetBool(DefendHash, Input);
        weaponAnimator.SetBool(DefendHash, Input);
    }
}