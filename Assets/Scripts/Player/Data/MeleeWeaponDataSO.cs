using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeWeaponData", menuName = "Data/Weapon Data/Melee Weapon")]
public class MeleeWeaponDataSO : WeaponDataSO
{
    [SerializeField] private WeaponAttackDetails[] weaponDetails;

    public WeaponAttackDetails[] WeaponDetails => weaponDetails;

    private void OnEnable()
    {
        var length = weaponDetails.Length;
        
        amountOfAttacks = length;
        
        movementSpeed = new float[length];
        for (var i = 0; i < length; i++)
        {
            movementSpeed[i] = weaponDetails[i].movementSpeed;
        }
    }
}