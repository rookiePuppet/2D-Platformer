using UnityEngine;

[CreateAssetMenu(fileName = "newRangedWeaponData", menuName = "Data/Weapon Data/Ranged Weapon")]
public class RangedWeaponDataSO : WeaponDataSO
{
    public ProjectileBehaviourType projectileType;
    public GameObject projectilePrefab;
    public Vector2 projectileInitialPosition;
    public float projectileInitialVelocity;
}

public enum ProjectileBehaviourType
{
    Fixed, // 定向
    Tracked // 追踪
}