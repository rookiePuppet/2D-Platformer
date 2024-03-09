using UnityEngine;

public class WeaponHitboxToWeapon : MonoBehaviour
{
    private MeleeWeapon _meleeWeapon;

    private void Awake()
    {
        _meleeWeapon = GetComponentInParent<MeleeWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _meleeWeapon.AddToDetected(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _meleeWeapon.RemoveFromDetected(other);
    }
}
