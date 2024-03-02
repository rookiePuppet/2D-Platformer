using UnityEngine;

public class InteractWithDoor : MonoBehaviour
{
    private PlayerInputHandler InputHandler => GetComponent<PlayerInputHandler>();

    private bool _waitingForInput;

    private Door _door;

    private void Update()
    {
        if (!_waitingForInput) return;
        
        if (InputHandler.InteractInput)
        {
            if (_door.IsActive)
            {
                _door.Deactivate();
            }
            else
            {
                _door.Activate();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Door>(out _door))
        {
            _waitingForInput = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Door>(out _door))
        {
            _waitingForInput = false;
        }
    }
}