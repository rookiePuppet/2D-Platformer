using UnityEngine;
using UnityEngine.UIElements;

public class StartController : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    
    private VisualElement _root;

    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        var startButton = _root.Q<Button>("StartButton");
        startButton.clicked += OnStartButtonClicked;
        
        var exitButton = _root.Q<Button>("ExitButton");
        exitButton.clicked += Application.Quit;
    }

    private void OnStartButtonClicked()
    {
        uiManager.LoadView(View.LevelSelectView);
    }
}