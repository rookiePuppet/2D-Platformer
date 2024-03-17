using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CombatDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticlesPrefab;
    [FormerlySerializedAs("healthBarUI")] [SerializeField] private HealthBarController healthBarController;
    public float Health { get; set; } = 100f;
    public event Action<DamageInfo> Damaged;

    private Animator _animator;
    private SpriteRenderer _mainSpriteRenderer;

    private static readonly int HitFromLeftHash = Animator.StringToHash("HitFromLeft");
    private static readonly int BeHitHash = Animator.StringToHash("BeHit");

    private void OnEnable()
    {
        Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        Damaged -= OnDamaged;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mainSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        UpdateHealthBar();
    }

    private void OnDamaged(DamageInfo info)
    {
        UpdateHealthBar();

        if (Health <= 0) return;

        // 受击动画
        var position = transform.position;
        var isHitFromLeft = info.hitSourcePosition.x < position.x;
        _animator.SetBool(HitFromLeftHash, isHitFromLeft);
        _animator.SetTrigger(BeHitHash);

        position.x = isHitFromLeft ? position.x + 0.1f : position.x - 0.1f;
        transform.position = position;

        // 打击粒子动画
        if (hitParticlesPrefab != null)
        {
            Instantiate(hitParticlesPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }

    public void TakeDamage(DamageInfo info)
    {
        if (Health <= 0) return;

        Health -= info.damageAmount;
        
        if (Health < 0)
        {
            Health = 0;
        }

        if (Health == 0)
        {
            _mainSpriteRenderer.enabled = false;
            Invoke(nameof(Recover), 5f);
        }
        
        Damaged?.Invoke(info);
    }

    private void UpdateHealthBar()
    {
        healthBarController.SetHealthBar(Health, 100f);
    }

    private void Recover()
    {
        Health = 100f;
        UpdateHealthBar();
        _mainSpriteRenderer.enabled = true;
    }
}