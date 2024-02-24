using UnityEngine;
using UnityEngine.Events;

public interface IDamageable
{
    float Health { get; set; }
    UnityEvent<float, float> HealthChanged { get; }
    
    void TakeDamage(float damage);

    void HandleHitFeedback(Vector3 hitSourcePos);
}