using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Basic Settings")]
    public float perceptionRadius = 6f;
    public float movementSpeed = 3f;
    public float edgeCheckDistance = 0.5f;
    
    [Header("Patrol Settings")]
    public float patrolRadius = 5f;
    public bool isPatrol;
    
    [Header("Attack Settings")]
    public float damage = 10f;
    public float attackRadius = 0.65f;
    public float attackCooldown = 2f;
    
    [Header("Chase Settings")]
    public float stopDistance = 1.2f;
    public float chaseSpeedMultiplier = 1.5f;
    public float targetLostWaitingTime = 3f;
    
    [Header("Layer Info")]
    public LayerMask playerLayer;
    public LayerMask groundLayer;
}