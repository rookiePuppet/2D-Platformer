using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    public Core Core { get; private set; }

    protected virtual void Awake()
    {
        Core = GetComponentInParent<Core>();
    }
}