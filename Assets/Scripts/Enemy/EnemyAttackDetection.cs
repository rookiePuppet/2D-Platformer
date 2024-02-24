using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool _shouldDetect;

    public void StartDetect()
    {
        _shouldDetect = true;
    }
    
    public void StopDetect()
    {
        _shouldDetect = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (!_shouldDetect) return;
        
        UnityEngine.Debug.Log("EnemyAttack: OnTriggerEnter");
        
        var damageable = other.GetComponent<IDamageable>();
        damageable?.TakeDamage(10f);
    }
}