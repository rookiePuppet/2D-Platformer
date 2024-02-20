using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }
    public CollisionSenses CollisionSenses { get; private set; }

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();

        if (Movement == null)
        {
            Debug.LogError("Movement component not found in children of Core");
        }

        if (CollisionSenses == null)
        {
            Debug.LogError("CollisionSenses component not found in children of Core");
        }
    }
}