using UnityEngine;

public interface IDamageable
{
    float Health { get; set; }
    
    void TakeDamage(float damage);

    void HandleHitFeedback(Vector3 hitSourcePos);
}