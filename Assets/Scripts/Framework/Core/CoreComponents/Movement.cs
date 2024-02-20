using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D Rigidbody { get; private set; }
    public Vector2 CurrentVelocity => Rigidbody.velocity;
    public int FacingDirection { get; private set; } = 1;

    private Vector2 _velocity;

    protected override void Awake()
    {
        base.Awake();
        Rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    public void CheckIfShouldFlip(int inputX)
    {
        if (inputX != 0 && inputX != FacingDirection)
        {
            Flip();
        }

        return;

        void Flip()
        {
            FacingDirection *= -1;
            Rigidbody.transform.Rotate(Vector3.up, 180F);
        }
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _velocity.x = angle.x * direction * velocity;
        _velocity.y = angle.y * velocity;
        Rigidbody.velocity = _velocity;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _velocity.x = direction.x * velocity;
        _velocity.y = direction.y * velocity;
        Rigidbody.velocity = _velocity;
    }

    public void SetVelocity(float x, float y)
    {
        _velocity.x = x;
        _velocity.y = y;

        Rigidbody.velocity = _velocity;
    }

    public void SetVelocityX(float velocityX) => SetVelocity(velocityX, CurrentVelocity.y);

    public void SetVelocityY(float velocityY) => SetVelocity(CurrentVelocity.x, velocityY);
}
