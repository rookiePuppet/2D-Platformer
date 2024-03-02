using UnityEngine;
using UnityEngine.UIElements;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private SceneLoaderSO sceneLoader;

    private VisualElement _root;

    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        var startButton = _root.Q<Button>("StartButton");
        startButton.clicked += LoadMainScene;
        
        var exitButton = _root.Q<Button>("ExitButton");
        exitButton.clicked += Application.Quit;
    }
    
    private void LoadMainScene()
    {
        StartCoroutine(sceneLoader.LoadSceneAsync("MainScene"));
    }
}