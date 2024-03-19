using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private bool _isHolding;
    private Vector2 _dashDirection;

    public PlayerDashState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();

        startTime = Time.unscaledTime;
        _isHolding = true;
        owner.PlayerStats.IsDashEnergyRecoverStopped = true;

        owner.DashDirectionIndicator.gameObject.SetActive(true);
        core.Movement.Rigidbody.drag = owner.StatesConfigSo.dashDrag;

        // 慢动作
        Time.timeScale = owner.StatesConfigSo.dashTimeScale;
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
                owner.PlayerStats.ConsumeDashEnergy();
                
                _isHolding = false;
                Time.timeScale = 1f;
                startTime = Time.time;
            }
            // 时间结束，终止动作
            else if (Time.unscaledTime >= startTime + owner.StatesConfigSo.dashHoldTime)
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
            core.Movement.SetVelocity(owner.StatesConfigSo.dashVelocity, _dashDirection);
            // 冲刺时间结束
            if (Time.time >= startTime + owner.StatesConfigSo.dashTime)
            {
                isAbilityDone = true;
                core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * owner.StatesConfigSo.dashEndYMultiplier);
            }
        }

        owner.Animator.SetFloat(velocityXHash, Mathf.Abs(core.Movement.CurrentVelocity.x));
        owner.Animator.SetFloat(velocityYHash, core.Movement.CurrentVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        core.Movement.Rigidbody.drag = 0f;
        owner.PlayerStats.IsDashEnergyRecoverStopped = false;
    }
}
