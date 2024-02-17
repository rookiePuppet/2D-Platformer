using System.Collections;
using System.Collections.Generic;
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
}
