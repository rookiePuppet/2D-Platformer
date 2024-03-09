using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
public class WeaponDataSO : ScriptableObject
{
    public int amountOfAttacks;
    public float[] movementSpeed;
}
