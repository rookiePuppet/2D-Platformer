using UnityEngine;

public class ScreenControllerView : MonoBehaviour, IUIBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}