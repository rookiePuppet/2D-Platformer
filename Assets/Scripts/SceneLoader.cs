using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "Scene Management/Scene Loader")]
public class SceneLoader : ScriptableObject
{
    [SerializeField] private UIManager uiManger;
    [SerializeField] private LevelInitializer levelInitializer;
    [SerializeField] private int simulateWaitingTime = 2;

    public async Awaitable LoadSceneAsync(string sceneName, Action onSceneLoaded = null)
    {
        uiManger.LoadUI<LoadingView>();

        await Awaitable.WaitForSecondsAsync(simulateWaitingTime);
        await SceneManager.LoadSceneAsync(sceneName);
        
        onSceneLoaded?.Invoke();
        
        uiManger.ClearCache();
    }

    public async Awaitable LoadLevelAsync(GameLevelData levelData)
    {
        await LoadSceneAsync(levelData.scene.name,
            () => { levelInitializer.InitializeLevel(levelData, () => { uiManger.LoadUI<MainView>(); }); });
    }
}