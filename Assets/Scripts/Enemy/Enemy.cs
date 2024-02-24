using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable
{
    public float perceptionRadius = 5f;
    public float patrolRadius = 10f;
    public float movementSpeed = 3f;
    public float stopDistance = 2f;
    public float attackCooldown = 2f;
    public float damage = 10f;
    public float attackRadius = 1.5f;

    public LayerMask playerLayer;

    public Transform attackPoint;
    [SerializeField] private HealthBarUI healthBarUI;

    public Animator Animator { get; private set; }
    public Core Core { get; private set; }
    public PlayerController Target { get; private set; }

    private StateMachine<Enemy> StateMachine { get; set; }
    public float Health { get; set; } = 100f;
    public event Action<float, float> HealthChanged;

    private void OnEnable()
    {
        HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        HealthChanged -= OnHealthChanged;
    }

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StateMachine = new EnemyStateMachine(this);
        HealthChanged?.Invoke(Health, 100f);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void OnHealthChanged(float health, float maxHealth)
    {
        healthBarUI.SetHealthBar(health, maxHealth);
    }
    
    public void TakeDamage(float damage)
    {
        Health -= damage;
        HealthChanged?.Invoke(Health, 100f);
    }

    public void HandleHitFeedback(Vector3 hitSourcePos)
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position - Vector3.right * patrolRadius,
            transform.position + Vector3.right * patrolRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(perceptionRadius * 2, 1f, 0f));
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public bool IsPlayerDetected()
    {
        var hit = Physics2D.OverlapBox(transform.position, new Vector2(perceptionRadius * 2, 1f), 0f, playerLayer);
        if (hit is not null)
        {
            Target = hit.GetComponent<PlayerController>();
        }

        return hit;
    }
    
    public void CheckMeleeAttack()
    {
        var target = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        if (target is not null)
        {
            var damageable = target.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
        }
    }
    
    public void AnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }
    
    public void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }
}