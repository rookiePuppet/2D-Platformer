using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData_SO enemyData;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform touchGroundPoint;
    [SerializeField] private HealthBarUI healthBarUI;

    public Animator Animator { get; private set; }
    public Core Core { get; private set; }
    public PlayerController Target { get; private set; }
    public EnemyData_SO Data => enemyData;

    private StateMachine<Enemy> StateMachine { get; set; }

    public float Health { get; set; } = 100f;
    public event Action<DamageInfo> Damaged;

    /// <summary>
    /// 检测前方是否为地面边缘
    /// </summary>
    public bool IsEdgeDetected => Physics2D.OverlapCircle(
        (Vector2)touchGroundPoint.position + Vector2.right * Core.Movement.FacingDirection * Data.edgeCheckDistance,
        0.2f,
        Data.groundLayer) is null;

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
        Core = GetComponentInChildren<Core>();
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StateMachine = new EnemyStateMachine(this);
        healthBarUI.SetHealthBar(Health, 100f);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public bool IsPlayerDetected()
    {
        var hit = Physics2D.OverlapBox(transform.position, new Vector2(Data.perceptionRadius * 2, 1f), 0f,
            Data.playerLayer);
        if (hit is not null)
        {
            Target = hit.GetComponent<PlayerController>();
        }

        return hit;
    }

    private void OnDamaged(DamageInfo info)
    {
        healthBarUI.SetHealthBar(Health, 100f);
        var direction = info.hitSourcePosition.x > transform.position.x ? -1 : 1;
        Core.Movement.SetVelocity(info.knockBackVelocity.x * direction, info.knockBackVelocity.y);
    }

    public void TakeDamage(DamageInfo info)
    {
        if (Health <= 0) return;

        Health -= info.damageAmount;

        if (Health <= 0)
        {
            Die();
        }

        Damaged?.Invoke(info);
    }

    public void CheckMeleeAttack()
    {
        var target = Physics2D.OverlapCircle(attackPoint.position, Data.attackRadius, Data.playerLayer);
        if (target is not null)
        {
            var damageable = target.GetComponent<IDamageable>();
            var damageInfo = new DamageInfo
            {
                damageAmount = Data.damage,
                hitSourcePosition = transform.position,
                knockBackVelocity = new Vector2(1.5f, 1f)
            };
            damageable?.TakeDamage(damageInfo);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void AnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    public void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position - Vector3.right * Data.patrolRadius,
            transform.position + Vector3.right * Data.patrolRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Data.perceptionRadius * 2, 1f, 0f));
        Gizmos.DrawWireSphere(attackPoint.position, Data.attackRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere((Vector2)touchGroundPoint.position + Vector2.right * Data.edgeCheckDistance, 0.2f);
    }
}