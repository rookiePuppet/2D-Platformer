using System;

public interface IDamageable
{
    public float Health { get; set; }
    public event Action<DamageInfo> Damaged;
    public event Action Dead;

    public void TakeDamage(DamageInfo info);
}