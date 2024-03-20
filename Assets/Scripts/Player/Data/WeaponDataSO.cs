using System;
using UnityEngine;

public class WeaponDataSO : ItemDataSO
{
    public WeaponType weaponType;
    public GameObject weaponPrefab;

    protected virtual void OnEnable()
    {
        itemType = ItemType.Weapon;
    }
}

public enum WeaponType
{
    Aggressive,
    Defensive
}