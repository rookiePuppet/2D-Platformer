using UnityEngine;
using UnityEngine.UIElements;

public class StartView : View
{
    [SerializeField] private UIManager uiManager;
    
    private void Start()
    {
        var startButton = Root.Q<Button>("StartButton");
        startButton.clicked += OnStartButtonClicked;
        
        var exitButton = Root.Q<Button>("ExitButton");
        exitButton.clicked += Application.Quit;
    }

    private void OnStartButtonClicked()
    {
        uiManager.LoadUI<LevelSelectView>();
    }
}