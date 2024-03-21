using UnityEngine;

public class StartSceneInitializer : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    void Start()
    {
        uiManager.LoadUI<StartView>();
    }
}