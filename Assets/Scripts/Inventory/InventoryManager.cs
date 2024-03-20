using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "InventoryManager", menuName = "Tool/InventoryManger")]
public class InventoryManager : ScriptableObject
{
    [field: SerializeField] public List<ItemDataSO> ItemsData { get; private set; }

    [SerializeField] private GameObject itemPrefab;

    [FormerlySerializedAs("uiManger")] [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerStatsSO playerStats;

    public PlayerController Player { private get; set; }
    private Weapon[] Weapons { get; set; }
    public bool IsPrimaryWeaponExists => Weapons[(int)CombatInputs.Primary] != null;
    public bool IsSecondaryWeaponExists => Weapons[(int)CombatInputs.Secondary] != null;
    public Weapon PrimaryWeapon => Weapons[(int)CombatInputs.Primary];
    public Weapon SecondaryWeapon => Weapons[(int)CombatInputs.Secondary];

    public event Action<Weapon, Weapon> WeaponChanged;

    public void InitializeWeapon(WeaponDataSO weaponData)
    {
        Weapons = new Weapon[2];

        var weaponObj = Instantiate(weaponData.weaponPrefab, Player.WeaponHolderTransform);
        Weapons[(int)CombatInputs.Primary] = weaponObj.GetComponent<Weapon>();

        WeaponChanged?.Invoke(PrimaryWeapon, SecondaryWeapon);
    }

    public void ChangeWeapon(WeaponDataSO weaponData, CombatInputs weaponOrder)
    {
        if (IsWeaponExists(weaponOrder, out var oldWeapon))
        {
            Destroy(oldWeapon.gameObject);
            Weapons[(int)weaponOrder] = null;
            InstantiateItem(oldWeapon.WeaponData, Player.transform.position);
        }

        var weaponObj = Instantiate(weaponData.weaponPrefab, Player.WeaponHolderTransform);
        Weapons[(int)weaponOrder] = weaponObj.GetComponent<Weapon>();

        WeaponChanged?.Invoke(PrimaryWeapon, SecondaryWeapon);
    }

    private void InstantiateItem(ItemDataSO itemData, Vector2 position)
    {
        var itemObj = Instantiate(itemPrefab, position, Quaternion.identity);
        var item = itemObj.GetComponent<Item>();
        item.Initialize(itemData);
    }

    private bool IsWeaponExists(CombatInputs weaponOrder, out Weapon weapon)
    {
        weapon = Weapons[(int)weaponOrder];
        var isWeaponExists = weapon != null;
        return isWeaponExists;
    }

    public void PickUpItem(Item item)
    {
        var itemData = item.Data;

        Action<Item> useAction = itemData.itemType switch
        {
            ItemType.Potion => UsePotion,
            ItemType.Weapon => UseWeapon,
            _ => null
        };
        
        useAction?.Invoke(item);
    }

    private void UsePotion(Item item)
    {
        var potionData = item.Data as PotionDataSO;
        if (potionData == null) return;

        if (potionData.gainType == PotionGainType.Health)
        {
            playerStats.RecoverHealth(potionData.gainValue);
        }

        Destroy(item.gameObject);
    }

    private void UseWeapon(Item item)
    {
        Player.StateMachine.TransitionTo<PlayerIdleState>();
        Player.InputHandler.GameplayActionMap.Disable();
        
        var weaponChangeWindow = uiManager.LoadUI<WeaponChangeWindow>();
        weaponChangeWindow.SetAlternativeWeapon(item);

        weaponChangeWindow.Closed += Player.InputHandler.GameplayActionMap.Enable;
    }
}