using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeathDrop : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private List<DropItemWithProbability> data;

    private IDamageable _damageable;

    private void Awake()
    {
        _damageable = GetComponent<IDamageable>();
    }

    private void OnEnable()
    {
        _damageable.Dead += DropItem;
    }

    private void OnDisable()
    {
        _damageable.Dead -= DropItem;
    }

    private void DropItem()
    {
        foreach (var item in data)
        {
            var random = Random.Range(0, item.probability);

            if (random > item.probability) return;

            inventoryManager.InstantiateItem(item.dropItem, transform.position);
        }
    }
}

[Serializable]
public struct DropItemWithProbability
{
    public ItemDataSO dropItem;
    [Range(0, 1f)] public float probability;
}