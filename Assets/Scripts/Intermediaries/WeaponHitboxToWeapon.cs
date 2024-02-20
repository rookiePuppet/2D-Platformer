using UnityEngine;

public class WeaponHitboxToWeapon : MonoBehaviour
{
    private AggressiveWeapon _aggressiveWeapon;

    private void Awake()
    {
        _aggressiveWeapon = GetComponentInParent<AggressiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _aggressiveWeapon.AddToDetected(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _aggressiveWeapon.RemoveFromDetected(other);
    }
}
