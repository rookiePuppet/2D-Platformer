public class AggressiveWeaponDataSO : WeaponDataSO
{
    public int amountOfAttacks;
    public float[] movementSpeed;

    protected override void OnEnable()
    {
        base.OnEnable();
        weaponType = WeaponType.Aggressive;
    }
}