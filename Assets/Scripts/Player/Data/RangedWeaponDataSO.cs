using UnityEngine;

[CreateAssetMenu(fileName = "newRangedWeaponData", menuName = "Data/Weapon Data/Ranged Weapon")]
public class RangedWeaponDataSO : AggressiveWeaponDataSO
{
    public ProjectileBehaviourType projectileType;
    public GameObject projectilePrefab;
    public Vector2 projectileInitialPosition;
    public float projectileInitialVelocity;
    public float damageAmount;
}

public enum ProjectileBehaviourType
{
    Fixed, // 定向
    Tracked // 追踪
}