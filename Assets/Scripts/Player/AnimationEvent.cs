using UnityEngine;

public class AnimationEvent : MonoBehaviour {
    private PlayerController _player;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
    }

    public void AnimationTrigger()
    {
        _player.StateMachine.CurrentState.AnimationTrigger();
    }

    public void AnimationFinishTrigger()
    {
        _player.StateMachine.CurrentState.AnimationFinishTrigger();
    }
}