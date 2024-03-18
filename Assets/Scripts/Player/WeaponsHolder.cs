using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsHolder : MonoBehaviour
{
    [SerializeField] private List<WeaponDataSO> weaponDataList;

    public Weapon[] Weapons { get; private set; }
    public bool IsPrimaryWeaponExists => Weapons[(int)CombatInputs.Primary] != null;
    public bool IsSecondaryWeaponExists => Weapons[(int)CombatInputs.Secondary] != null;

    public event Action WeaponChanged;

    public void InitializeWeapon(int id)
    {
        Weapons = new Weapon[2];

        var weaponData = weaponDataList.Find(data => data.id == id);
        var weaponObj = Instantiate(weaponData.weaponPrefab, transform);
        Weapons[(int)CombatInputs.Primary] = weaponObj.GetComponent<Weapon>();

        WeaponChanged?.Invoke();
    }
}