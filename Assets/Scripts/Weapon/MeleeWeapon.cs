using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : AggressiveWeapon
{
    private MeleeWeaponDataSO _meleeWeaponData;

    private readonly List<IDamageable> _detectedTargets = new();

    protected override void Awake()
    {
        base.Awake();

        _meleeWeaponData = aggressiveWeaponData as MeleeWeaponDataSO;

        if (_meleeWeaponData != null)
        {
            var amountOfAttacks = _meleeWeaponData.WeaponDetails.Length;
            aggressiveWeaponData.amountOfAttacks = amountOfAttacks;

            var movementSpeeds = new float[amountOfAttacks];

            for (var i = 0; i < amountOfAttacks; i++)
            {
                movementSpeeds[i] = _meleeWeaponData.WeaponDetails[i].movementSpeed;
            }

            aggressiveWeaponData.movementSpeed = movementSpeeds;
        }
    }

    private void CheckMeleeAttack()
    {
        foreach (var target in _detectedTargets)
        {
            if (target == null) continue;
            
            var weaponDetail = _meleeWeaponData.WeaponDetails[attackCounter - 1];
            var damageInfo = new DamageInfo
            {
                damageAmount = weaponDetail.damageAmount,
                hitSourcePosition = transform.position,
                knockBackVelocity = weaponDetail.knockBackForce
            };
            target.TakeDamage(damageInfo);
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