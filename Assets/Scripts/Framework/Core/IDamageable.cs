using System;
using UnityEngine;
using UnityEngine.Events;

public interface IDamageable
{
    public float Health { get; set; }
    public event Action<float, float> HealthChanged;
    public void TakeDamage(float damage);
    public void HandleHitFeedback(Vector3 hitSourcePos);
}