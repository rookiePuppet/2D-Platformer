using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryManager", menuName = "Tool/InventoryManger")]
public class InventoryManager : ScriptableObject
{
    [field: SerializeField] public List<ItemDataSO> ItemsData { get; private set; }

    [SerializeField] private PlayerStatsSO playerStats;

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

    public void UsePotion(Item item)
    {
        var potionData = item.Data as PotionDataSO;
        if (potionData == null) return;

        if (potionData.gainType == PotionGainType.Health)
        {
            playerStats.RecoverHealth(potionData.gainValue);
        }

        Destroy(item.gameObject);
    }

    public void UseWeapon(Item item)
    {
    }
}