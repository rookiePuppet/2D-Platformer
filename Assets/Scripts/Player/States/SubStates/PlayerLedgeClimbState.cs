using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 _cornerPos;
    private Vector2 _startPos;
    private Vector2 _stopPos;

    private bool _isHanging;
    private bool _isClimbing;
    
    public PlayerLedgeClimbState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        owner.SetVelocity(0f, 0f);
        
        _cornerPos = owner.DetermineCornerPosition();

        _startPos = _cornerPos - new Vector2(owner.Data.startOffset.x * owner.FacingDirection, owner.Data.startOffset.y);
        _stopPos = _cornerPos + new Vector2(owner.Data.stopOffset.x * owner.FacingDirection, owner.Data.stopOffset.y);
       
        owner.transform.position = _startPos;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if(InputX == 0) stateMachine.TransitionTo<PlayerIdleState>();
            else stateMachine.TransitionTo<PlayerWalkState>();
        }
        else
        {
            owner.SetVelocity(0f, 0f);
            owner.transform.position = _startPos;
            
            if ((InputX == owner.FacingDirection || InputY > 0) && _isHanging && !_isClimbing)
            {
                _isClimbing = true;
                owner.Animator.SetBool(climbLedgeHash, true);
            } else if (InputY < 0 && _isHanging && !_isClimbing)
            {
                stateMachine.TransitionTo<PlayerAerialState>();
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        _isHanging = false;

        if (_isClimbing)
        {
            owner.transform.position = _stopPos;
            _isClimbing = false;
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        _isHanging = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        owner.Animator.SetBool(climbLedgeHash, false);
    }
}
