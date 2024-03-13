using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeWeaponData", menuName = "Data/Weapon Data/Melee Weapon")]
public class MeleeWeaponDataSO : AggressiveWeaponDataSO
{
    [SerializeField] private WeaponAttackDetails[] weaponDetails;

    public WeaponAttackDetails[] WeaponDetails => weaponDetails;
}