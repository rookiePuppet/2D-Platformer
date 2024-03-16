using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "Scene Management/Scene Loader")]
public class SceneLoaderSO : ScriptableObject
{
    [SerializeField] private UIManager UIManger;
    [SerializeField] private int simulateWaitingTime = 2;

    public async Awaitable LoadSceneAsync(string sceneName, Action onSceneLoaded = null)
    {
        UIManger.LoadView(View.LoadingView);

        await Awaitable.WaitForSecondsAsync(simulateWaitingTime);
        await SceneManager.LoadSceneAsync(sceneName);
        
        onSceneLoaded?.Invoke();
    }
}