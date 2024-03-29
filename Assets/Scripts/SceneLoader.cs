using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "Tool/Scene Loader")]
public class SceneLoader : ScriptableObject
{
    [SerializeField] private UIManager uiManger;
    [SerializeField] private LevelInitializer levelInitializer;
    [SerializeField] private int simulateWaitingTime = 2;

    [SerializeField] private GameLevelsDataSO gameLevels;

    public async Awaitable LoadSceneAsync(string sceneName, Action onSceneLoaded = null)
    {
        uiManger.LoadUI<LoadingView>();

        await Awaitable.WaitForSecondsAsync(simulateWaitingTime);
        await SceneManager.LoadSceneAsync(sceneName);

        uiManger.ClearCache();
        
        onSceneLoaded?.Invoke();
    }

    public async Awaitable LoadLevelAsync(GameLevelData levelData)
    {
        await LoadSceneAsync(levelData.sceneName,
            () =>
            {
                levelInitializer.InitializeLevel(levelData, () =>
                {
                    uiManger.LoadUI<MainView>();
                    ShowControllerViewIfMobile();
                });
            });
    }

    public async Awaitable LoadLevelByIdAsync(int sceneId)
    {
        var levelData = gameLevels.data.Find((level) => level.id == sceneId);

        await LoadSceneAsync(levelData.sceneName,
            () =>
            {
                levelInitializer.InitializeLevel(levelData, () =>
                {
                    uiManger.LoadUI<MainView>();
                    ShowControllerViewIfMobile();
                });
            });
    }

    private void ShowControllerViewIfMobile()
    {
#if UNITY_ANDROID || UNITY_IOS
        uiManger.LoadUI<ScreenControllerView>();
#endif
    }
}