using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private int destroyTime = 3;

    [SerializeField] private float checkRadius;

    private float _velocity;

    private ProjectileBehaviourType _type;
    private bool _isFromEnemy;
    private bool _isActive;

    private Action _releaseAction;

    private Transform _trackedTarget;
    private bool _isReleased;

    public void Init(ProjectileBehaviourType type, float velocity, Action releaseAction, bool isFromEnemy = false)
    {
        _type = type;
        _velocity = velocity;
        _isFromEnemy = isFromEnemy;
        _releaseAction = releaseAction;

        _isActive = true;
        _isReleased = false;

        Invoke(nameof(Release), destroyTime);
    }

    private void Update()
    {
        if (!_isActive) return;

        if (_type == ProjectileBehaviourType.Fixed)
        {
            HandleFixedBehaviour();
        }
        else
        {
            HandleTrackedBehaviour();
        }
    }

    private void SearchForTarget()
    {
        var colliders =
            Physics2D.OverlapCircleAll(transform.position, checkRadius, 1 << LayerMask.NameToLayer("Damageable"));

        foreach (var item in colliders)
        {
            print(item.transform.name);
            if (item != null && !item.CompareTag("Player"))
            {
                _trackedTarget = item.transform;
                return;
            }
        }
    }

    private void HandleTrackedBehaviour()
    {
        if (_trackedTarget is null)
        {
            SearchForTarget();
        }

        if (_trackedTarget is null)
        {
            return;
        }

        var direction = _trackedTarget.position - transform.position;
        transform.position += Time.deltaTime * _velocity * direction;
    }

    private void HandleFixedBehaviour()
    {
        transform.position += _velocity * Time.deltaTime * transform.right;
    }

    private void HandleDamage(Collider2D other)
    {
        var damageable = other.GetComponent<IDamageable>();
        damageable?.TakeDamage(new DamageInfo
        {
            damageAmount = damage,
            hitSourcePosition = transform.position
        });

        _velocity = 0;
        Release();
    }

    private void Release()
    {
        if (_isReleased) return;

        _releaseAction?.Invoke();
        _isReleased = true;
        _trackedTarget = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_isFromEnemy)
            {
                HandleDamage(other);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (!_isFromEnemy)
            {
                HandleDamage(other);
            }
        }
        else
        {
            HandleDamage(other);
        }
    }
}