using UnityEngine;
using UnityEngine.UIElements;

public class StartView : View
{
    [SerializeField] private UIManager uiManager;

    private void OnEnable()
    {
        var startButton = Root.Q<Button>("StartButton");
        startButton.clicked += OnStartButtonClicked;
        
        var exitButton = Root.Q<Button>("ExitButton");
        exitButton.clicked += Application.Quit;
    }

    private void Start()
    {
        Root.Q<Button>("StartButton").Focus();
    }

    private void OnStartButtonClicked()
    {
        uiManager.LoadUI<LevelSelectView>();
        Hide();
    }
}