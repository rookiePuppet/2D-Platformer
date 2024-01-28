using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Data/Base Data", fileName = "newPlayerData")]
public class PlayerData : ScriptableObject {
    [Header("Walk State")] 
    public float movementVelocity = 10f;

    [Header("Jump State")] 
    public float jumpVelocity = 10f;
    public float amountOfJumps = 2;

    [Header("Aerial State")] 
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")] 
    public float wallSlideVelocity = 3f;
    
    [Header("Wall Climb State")] 
    public float wallClimbVelocity = 2f;

    [Header("Wall Jump State")] public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Check Variables")] 
    public float groundCheckRadius = 0.5f;
    public float wallCheckDistance = 1f;

    [Header("Layer Mask")]
    public LayerMask groundLayer;
}