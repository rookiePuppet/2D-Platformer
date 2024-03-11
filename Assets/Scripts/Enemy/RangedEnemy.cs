using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Vector2 projectileInitialPosition;
    [SerializeField] private float projectileInitialVelocity;

    private GameObject _projectileObj;

    public void PerformRangedAttack()
    {
        if (_projectileObj == null)
        {
            _projectileObj = Instantiate(projectilePrefab);
        }

        _projectileObj.SetActive(true);

        _projectileObj.transform.position = transform.position + (Vector3)projectileInitialPosition;
        _projectileObj.transform.rotation =
            Quaternion.Euler(0, 0, Core.Movement.FacingDirection == 1 ? 0 : -180);

        var projectile = _projectileObj.GetComponent<Projectile>();
        projectile.Init(ProjectileBehaviourType.Fixed, projectileInitialVelocity,
            () => projectile.gameObject.SetActive(false), true);
    }
}