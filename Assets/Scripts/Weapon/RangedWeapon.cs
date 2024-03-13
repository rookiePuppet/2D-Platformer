using UnityEngine;
using UnityEngine.Pool;

public class RangedWeapon : AggressiveWeapon
{
    private RangedWeaponDataSO _rangedWeaponData;
    private PlayerController _player;

    private ObjectPool<GameObject> _projectilePool;

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponentInParent<PlayerController>();
        
        _rangedWeaponData = weaponData as RangedWeaponDataSO;
    }

    protected override void Start()
    {
        base.Start();
        _projectilePool = new ObjectPool<GameObject>(CreateProjectile, OnGetProjectile, OnReleaseProjectile);
    }

    private void PerformRangedAttack()
    {
        var projectileObj = _projectilePool.Get();
        var projectile = projectileObj.GetComponent<Projectile>();
        projectile.Init(_rangedWeaponData.projectileType, _rangedWeaponData.projectileInitialVelocity,
            () => _projectilePool.Release(projectileObj));
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        PerformRangedAttack();
    }

    private GameObject CreateProjectile()
    {
        return Instantiate(_rangedWeaponData.projectilePrefab);
    }

    private void OnGetProjectile(GameObject projectileObj)
    {
        projectileObj.transform.parent = null;
        projectileObj.transform.position =
            _player.transform.position + (Vector3)_rangedWeaponData.projectileInitialPosition;
        projectileObj.transform.rotation =
            Quaternion.Euler(0, 0, _player.Core.Movement.FacingDirection == 1 ? 0 : -180);

        projectileObj.SetActive(true);
    }

    private void OnReleaseProjectile(GameObject projectileObj)
    {
        projectileObj.SetActive(false);
    }
}