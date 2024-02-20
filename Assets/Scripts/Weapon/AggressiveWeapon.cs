using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    private AggressiveWeaponData_SO _aggressiveWeaponData;

    private readonly List<IDamageable> _detectedTargets = new();

    private void OnEnable()
    {
        if (weaponData.GetType() == typeof(AggressiveWeaponData_SO))
        {
            _aggressiveWeaponData = (AggressiveWeaponData_SO)weaponData;
        }
        else
        {
            Debug.LogError("weapon data is not of type AggressiveWeaponData_SO");
        }
    }

    private void CheckMeleeAttack()
    {
        foreach (var target in _detectedTargets)
        {
            target.TakeDamage(_aggressiveWeaponData.WeaponDetails[attackCounter - 1].damageAmount);
            target.HandleHitFeedback(transform.position);
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
    }

    public void AddToDetected(Collider2D detectedCollider)
    {
        var damageable = detectedCollider.GetComponent<IDamageable>();

        if (damageable is not null)
        {
            _detectedTargets.Add(damageable);
        }
    }

    public void RemoveFromDetected(Collider2D detectedCollider)
    {
        var damageable = detectedCollider.GetComponent<IDamageable>();

        if (damageable is not null)
        {
            _detectedTargets.Remove(damageable);
        }
    }
}