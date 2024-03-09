using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private int destroyTime = 3;

    private float _velocity;

    private ProjectileBehaviourType _type;
    private bool _isFromEnemy;
    private bool _isActive;

    private Action _releaseAction;

    public void Init(ProjectileBehaviourType type, float velocity, Action releaseAction, bool isFromEnemy = false)
    {
        _velocity = velocity;
        _isFromEnemy = isFromEnemy;
        _releaseAction = releaseAction;

        _isActive = true;
        
        Invoke(nameof(Release), destroyTime);
    }

    private void Update()
    {
        if (!_isActive) return;

        if (_type == ProjectileBehaviourType.Fixed)
        {
            transform.position += _velocity * Time.deltaTime * transform.right ;
        }
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
        transform.SetParent(other.transform);
    }

    private void Release()
    {
        _releaseAction?.Invoke();
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
        else
        {
            HandleDamage(other);
        }
    }
}