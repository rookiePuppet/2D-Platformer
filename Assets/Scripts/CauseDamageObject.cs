using UnityEngine;

public class CauseDamageObject : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] private bool isDeadliness;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageable = other.GetComponent<IDamageable>();
        damageable?.TakeDamage(new DamageInfo
        {
            damageAmount = isDeadliness ? damageable.Health : damageAmount
        });
    }
}