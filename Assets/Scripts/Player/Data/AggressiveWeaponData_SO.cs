using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newAggressiveWeaponData", menuName = "Data/Weapon Data/Aggressive Weapon")]
public class AggressiveWeaponData_SO : WeaponData_SO
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