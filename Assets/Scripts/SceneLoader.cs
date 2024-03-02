using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "Scene Management/Scene Loader")]
public class SceneLoaderSO : ScriptableObject
{
    [SerializeField] private GameObject loadingScreenPrefab;
    [SerializeField] private float simulateWaitingTime = 2f;
    
    public IEnumerator LoadSceneAsync(string sceneName, Action onSceneLoaded = null)
    {
        var loadingScreen = Instantiate(loadingScreenPrefab);

        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        asyncLoad.completed += _ =>
        {
            Destroy(loadingScreen);
            onSceneLoaded?.Invoke();
        };
        
        yield return new WaitForSeconds(simulateWaitingTime);
        asyncLoad.allowSceneActivation = true;
    }
}
