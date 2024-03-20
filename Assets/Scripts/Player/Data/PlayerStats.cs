using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("生命值")]
    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100f;

    [Header("冲刺能量")]
    [SerializeField] private float dashEnergy = 20f;
    [SerializeField] private float maxDashEnergy = 20f;
    [SerializeField] private float dashConsumeEnergy = 10f;

    public float Health
    {
        get => health;
        set => health = value;
    }

    [field: SerializeField] public float DamageReductionRate { get; set; }

    public bool IsDashEnergyRecoverStopped { get; set; }

    public event Action<float, float> HealthChanged;
    public event Action<float, float> DashEnergyChanged;

    private void OnEnable()
    {
        health = maxHealth;
        dashEnergy = maxDashEnergy;
        DamageReductionRate = 0;
    }

    public bool IsDashAvailable => dashEnergy >= dashConsumeEnergy;

    public void ConsumeDashEnergy()
    {
        dashEnergy -= dashConsumeEnergy;
        DashEnergyChanged?.Invoke(dashEnergy, maxDashEnergy);
    }

    public void RecoverDashEnergy()
    {
        if (IsDashEnergyRecoverStopped) return;

        if (dashEnergy > maxDashEnergy)
        {
            dashEnergy = maxDashEnergy;
            IsDashEnergyRecoverStopped = true;
            return;
        }

        dashEnergy += Time.deltaTime;

        DashEnergyChanged?.Invoke(dashEnergy, maxDashEnergy);
    }

    public void RecoverHealth(float recoverValue)
    {
        if (health >= maxHealth) return;

        health += recoverValue;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        HealthChanged?.Invoke(health, maxHealth);
    }

    public void TakeDamage(DamageInfo info)
    {
        if (health <= 0) return;

        health -= info.damageAmount * (1 - DamageReductionRate);

        if (health < 0)
        {
            health = 0;
        }

        HealthChanged?.Invoke(health, maxHealth);
    }
}