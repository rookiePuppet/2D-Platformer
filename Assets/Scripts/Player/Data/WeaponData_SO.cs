using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
public class WeaponData_SO : ScriptableObject
{
    public int amountOfAttacks;
    public float[] movementSpeed;
}
