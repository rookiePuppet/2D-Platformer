using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "Scene Management/Scene Loader")]
public class SceneLoader : ScriptableObject
{
    [SerializeField] private UIManager uiManger;
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private int simulateWaitingTime = 2;

    public async Awaitable LoadSceneAsync(string sceneName, Action onSceneLoaded = null)
    {
        uiManger.LoadView(View.LoadingView);

        await Awaitable.WaitForSecondsAsync(simulateWaitingTime);
        await SceneManager.LoadSceneAsync(sceneName);

        onSceneLoaded?.Invoke();
    }

    public async Awaitable LoadLevelAsync(GameLevelData levelData)
    {
        await LoadSceneAsync(levelData.scene.name, () =>
        {
            var playerObject = Instantiate(playerPrefab, levelData.playerInitialPosition, Quaternion.identity);
            var camera = FindAnyObjectByType<CinemachineVirtualCamera>();
            camera.Follow = playerObject.transform;
        });
    }
}