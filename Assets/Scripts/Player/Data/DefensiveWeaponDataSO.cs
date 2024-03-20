using UnityEngine;

[CreateAssetMenu(fileName = "newDefensiveWeaponData", menuName = "Data/Weapon Data/Defensive Weapon")]
public class DefensiveWeaponDataSO : WeaponDataSO
{
    [Range(0, 1f)]
    public float damageReductionRate;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        weaponType = WeaponType.Defensive;
    }
}