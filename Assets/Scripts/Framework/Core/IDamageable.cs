using System;

public interface IDamageable
{
    public float Health { get; set; }
    public event Action<DamageInfo> Damaged;

    public void TakeDamage(DamageInfo info);
}