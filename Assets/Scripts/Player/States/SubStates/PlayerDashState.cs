using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash
    {
        get
        {
            if (Time.time - _lastDashTime < owner.Data.dashCoolDown)
            {
                return false;
            }

            return true;
        }
    }

    private bool _isHolding;
    private float _lastDashTime;
    private Vector2 _dashDirection;

    public PlayerDashState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();

        startTime = Time.unscaledTime;
        _isHolding = true;

        owner.DashDirectionIndicator.gameObject.SetActive(true);
        owner.Rigidbody.drag = owner.Data.dashDrag;

        // 慢动作
        Time.timeScale = owner.Data.dashTimeScale;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExiting) return;

        if (_isHolding)
        {
            _dashDirection = owner.InputHandler.RawDashDirectionInput;
            // 控制方向指示器旋转
            var angle = Vector2.SignedAngle(Vector2.right, _dashDirection); //direction和Vector2.right的夹角
            owner.DashDirectionIndicator.rotation = Quaternion.AngleAxis(angle - 45f, Vector3.forward);

            core.Movement.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));

            // 松开冲刺键，开始冲刺
            if (owner.InputHandler.DashInputStop)
            {
                _isHolding = false;
                Time.timeScale = 1f;
                startTime = Time.time;
            }
            // 时间结束，终止动作
            else if (Time.unscaledTime >= startTime + owner.Data.dashHoldTime)
            {
                Time.timeScale = 1f;
                isAbilityDone = true;
                owner.DashDirectionIndicator.gameObject.SetActive(false);
            }
        }
        // 开始冲刺
        else
        {
            owner.DashDirectionIndicator.gameObject.SetActive(false);
            core.Movement.SetVelocity(owner.Data.dashVelocity, _dashDirection);
            // 冲刺时间结束
            if (Time.time >= startTime + owner.Data.dashTime)
            {
                _lastDashTime = Time.time;
                isAbilityDone = true;
                core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * owner.Data.dashEndYMultiplier);
            }
        }

        owner.Animator.SetFloat(velocityXHash, Mathf.Abs(core.Movement.CurrentVelocity.x));
        owner.Animator.SetFloat(velocityYHash, core.Movement.CurrentVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        owner.Rigidbody.drag = 0f;
    }
}
