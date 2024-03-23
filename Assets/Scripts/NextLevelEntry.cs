using UnityEngine;

public class NextLevelEntry : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private int nextLevelId;

    [SerializeField] private bool returnToStartScene;

    private async void OnTriggerEnter2D(Collider2D other)
    {
        if (returnToStartScene)
        {
            await sceneLoader.LoadSceneAsync("StartScene");
            return;
        }

        await sceneLoader.LoadLevelByIdAsync(nextLevelId);
    }
}