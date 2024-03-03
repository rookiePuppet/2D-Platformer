using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 _cornerPos; // 墙角位置
    private Vector2 _startPos; // 爬墙起始位置
    private Vector2 _stopPos; // 爬墙结束位置

    private bool _isHanging; // 是否悬挂
    private bool _isClimbing; // 是否正在攀爬

    private bool _jumpInput; // 跳跃输入

    public PlayerLedgeClimbState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();

        core.Movement.SetVelocity(0f, 0f);

        // 确定角落位置
        _cornerPos = DetermineCornerPosition();

        // 确定爬墙起始位置和结束位置
        _startPos = _cornerPos - new Vector2(owner.StatesConfigSo.startOffset.x * core.Movement.FacingDirection, owner.StatesConfigSo.startOffset.y);
        _stopPos = _cornerPos + new Vector2(owner.StatesConfigSo.stopOffset.x * core.Movement.FacingDirection, owner.StatesConfigSo.stopOffset.y);

        // 设置角色到起始位置
        owner.transform.position = _startPos;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 攀爬动作播放完之后，切换到站立状态或行走状态
        if (isAnimationFinished)
        {
            if (IsTouchingCeiling())
            {
                stateMachine.TransitionTo<PlayerCrouchIdleState>();
            }
            else
            {
                stateMachine.TransitionTo<PlayerIdleState>();
            }
        }
        else
        {
            core.Movement.SetVelocity(0f, 0f);
            owner.transform.position = _startPos;

            _jumpInput = owner.InputHandler.JumpInput;

            // 按下右或上的方向键时，开始攀爬
            if ((InputX == core.Movement.FacingDirection || InputY > 0) && _isHanging && !_isClimbing)
            {
                _isClimbing = true;
                owner.Animator.SetBool(climbLedgeHash, true);
            }
            // 悬挂时跳墙
            else if (_jumpInput && !_isClimbing)
            {
                stateMachine.TransitionTo<PlayerWallJumpState>();
            }
            // 按下下方向键时，进入空中状态往下掉落
            else if (InputY < 0 && _isHanging && !_isClimbing)
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
            // 设置角色到结束位置
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

    public bool IsTouchingCeiling() => Physics2D.Raycast(_cornerPos + Vector2.up * 0.015f + Vector2.right * core.Movement.FacingDirection * 0.015f, Vector2.up, owner.StatesConfigSo.normalColliderHeight, core.CollisionSenses.GroundLayer);

    private Vector2 DetermineCornerPosition()
    {
        var wallCheckPos = core.CollisionSenses.WallCheck.position;
        var ledgeCheckPos = core.CollisionSenses.LedgeCheck.position;
        var direction = Vector2.right * core.Movement.FacingDirection;

        // 获得角色与墙壁的横向距离
        var wallHit = Physics2D.Raycast(wallCheckPos, direction, core.CollisionSenses.WallCheckDistance, core.CollisionSenses.GroundLayer);
        var xDis = wallHit.distance;

        // 获得头顶到地面的垂直距离
        var groundHit = Physics2D.Raycast((Vector2)ledgeCheckPos + direction * xDis,
            Vector2.down, ledgeCheckPos.y - wallCheckPos.y, core.CollisionSenses.GroundLayer);
        var yDis = groundHit.distance;

        return new Vector2(wallCheckPos.x + xDis * core.Movement.FacingDirection, ledgeCheckPos.y - yDis);
    }
}
