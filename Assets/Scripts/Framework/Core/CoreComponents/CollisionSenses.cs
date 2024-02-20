using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    public Transform GroundCheck => groundCheck;
    public Transform WallCheck => wallCheck;
    public Transform LedgeCheck => ledgeCheck;
    public Transform CeilingCheck => ceilingCheck;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform ceilingCheck;

    [SerializeField] private float groundCheckRadius = 0.25f;
    [SerializeField] private float wallCheckDistance = 0.4f;
    [SerializeField] private float ceilingCheckDistance = 0.5f;
    [SerializeField] private LayerMask groundLayer;

    public float GroundCheckRadius => groundCheckRadius;
    public float WallCheckDistance => wallCheckDistance;
    public float CeilingCheckDistance => ceilingCheckDistance;
    public LayerMask GroundLayer => groundLayer;

    public bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, GroundLayer);

    public bool IsTouchingWall => Physics2D.Raycast(wallCheck.position, Vector2.right * Core.Movement.FacingDirection,
        WallCheckDistance, GroundLayer);

    public bool IsTouchingLedge => Physics2D.Raycast(ledgeCheck.position, Vector2.right * Core.Movement.FacingDirection,
        WallCheckDistance, GroundLayer);

    public bool IsTouchingCeiling => Physics2D.OverlapCircle(ceilingCheck.position, GroundCheckRadius,
        GroundLayer);

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * wallCheckDistance);
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + Vector3.right * wallCheckDistance);
        Gizmos.DrawLine(ceilingCheck.position, ceilingCheck.position + Vector3.up * ceilingCheckDistance);
    }
}
