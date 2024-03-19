using System;
using UnityEngine;

public class WeaponDataSO : ItemDataSO
{
    public GameObject weaponPrefab;

    private void OnEnable()
    {
        itemType = ItemType.Weapon;
    }
}