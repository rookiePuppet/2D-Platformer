using UnityEngine;

[CreateAssetMenu(fileName = "newItemData", menuName = "Inventory/Item")]
public class ItemDataSO : ScriptableObject
{
    public int id;
    public ItemType itemType;
    public string itemName;
    public Sprite itemSprite;
}

public enum ItemType
{
    Weapon,
    Potion
}