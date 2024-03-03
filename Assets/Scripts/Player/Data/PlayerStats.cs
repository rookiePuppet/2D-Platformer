using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100f;

    public float Health => health;
    public float MaxHealth => maxHealth;

    public event Action HealthChanged;

    private void OnEnable()
    {
        health = maxHealth;
    }

    public void TakeDamage(DamageInfo info)
    {
        if (health <= 0) return;

        health -= info.damageAmount;
        
        if (health < 0)
        {
            health = 0;
        }
        
        HealthChanged?.Invoke();
    }
}