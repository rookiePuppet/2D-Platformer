using UnityEngine;
using Random = UnityEngine.Random;

public class CombatDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticlesPrefab;
    public float Health { get; set; } = 100f;

    private Animator _animator;
    private SpriteRenderer _mainSpriteRenderer;
    private static readonly int HitFromLeftHash = Animator.StringToHash("HitFromLeft");
    private static readonly int BeHitHash = Animator.StringToHash("BeHit");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mainSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (Health <= 0) return;

        Health -= damage;

        if (Health < 0)
        {
            Health = 0;
        }

        if (Health == 0)
        {
            _mainSpriteRenderer.enabled = false;
            Invoke(nameof(Recover), 5f);
        }
    }

    public void HandleHitFeedback(Vector3 hitSourcePos)
    {
        if(Health <= 0) return;
        
        // 受击动画
        var position = transform.position;
        var isHitFromLeft = hitSourcePos.x < position.x;
        _animator.SetBool(HitFromLeftHash, isHitFromLeft);
        _animator.SetTrigger(BeHitHash);
        
        position.x = isHitFromLeft ? position.x + 0.1f : position.x - 0.1f;

        // 打击粒子动画
        if (hitParticlesPrefab != null)
        {
            Instantiate(hitParticlesPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }

    private void Recover()
    {
        Health = 100f;
        _mainSpriteRenderer.enabled = true;
    }
}