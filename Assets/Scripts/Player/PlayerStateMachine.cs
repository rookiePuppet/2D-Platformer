﻿using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerController> {
    public PlayerStateMachine(PlayerController player) : base(player)
    {
        // 初始化状态机
        AddState(new PlayerIdleState(this, owner, Animator.StringToHash("Idle")));
        AddState(new PlayerWalkState(this, owner, Animator.StringToHash("Walk")));
        AddState(new PlayerJumpState(this, owner, Animator.StringToHash("Jump")));
        AddState(new PlayerAerialState(this, owner, Animator.StringToHash("Aerial")));
        AddState(new PlayerLandState(this, owner, Animator.StringToHash("Land")));
        AddState(new PlayerWallSlideState(this, owner, Animator.StringToHash("WallSlide")));
        AddState(new PlayerWallGrabState(this, owner, Animator.StringToHash("WallGrab")));
        AddState(new PlayerWallClimbState(this, owner, Animator.StringToHash("WallClimb")));

        Initialize<PlayerIdleState>();
    }
}