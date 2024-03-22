using UnityEngine;

public class DeathDrop : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;

    public void DropItem(ItemDataSO itemData, float probability)
    {
        var random = Random.Range(0, probability);

        if (random > probability) return;
        
        inventoryManager.InstantiateItem(itemData, transform.position);
    }
}