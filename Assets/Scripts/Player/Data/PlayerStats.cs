using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100f;

    [field: SerializeField] public float DamageReductionRate { get; set; }
    
    public float Health => health;
    public float MaxHealth => maxHealth;

    public event Action HealthChanged;

    private void OnEnable()
    {
        health = maxHealth;
        DamageReductionRate = 0;
    }

    public void TakeDamage(DamageInfo info)
    {
        if (health <= 0) return;

        health -= info.damageAmount * (1 - DamageReductionRate);
        
        if (health < 0)
        {
            health = 0;
        }
        
        HealthChanged?.Invoke();
    }
}