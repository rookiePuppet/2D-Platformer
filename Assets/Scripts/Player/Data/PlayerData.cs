using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Data/Base Data", fileName = "newPlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Walk State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 10f;
    public int amountOfJumps = 2;

    [Header("Aerial State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float airSteeringMultiplier = 0.75f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 2f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Dash State")]
    public float dashTime = 0.5f;
    public float dashVelocity = 15f;
    public float dashTimeScale = 0.2f;
    public float dashHoldTime = 1.5f;
    public float dashEndYMultiplier = 0.2f;
    public float dashDrag = 0.2f;
    public float dashCoolDown = 1f;

    [Header("Crouch State")]
    public float crouchMovementVelocity = 5f;
    public float normalColliderYOffset = -0.18f;
    public float normalColliderHeight = 1.6f;
    public float crouchColliderHeight = 1f;
}