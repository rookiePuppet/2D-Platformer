using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "UIManager", menuName = "UIManager")]
public class UIManager : ScriptableObject
{
    [SerializeField] private string rootPath;

    private readonly Dictionary<string, UIBase> _uis = new();

    public void LoadView(View view)
    {
        var uiName = view.ToString();
        if (_uis.TryGetValue(uiName, out var ui))
        {
            ui.Show();
        }
        else
        {
            var original = Resources.Load($"{rootPath}/{uiName}");
            if (original is null)
            {
                Debug.LogErrorFormat("The view you are trying to load is not found. Please check the prefab path.");
                return;
            }

            var gameObject = Instantiate(original);

            ui = gameObject.GetComponent<UIBase>();
            _uis[uiName] = ui;

            ui.Show();
        }
    }

    public void UnloadView(View view)
    {
        var uiName = view.ToString();
        if (_uis.TryGetValue(uiName, out var ui))
        {
            ui.Hide();

            Debug.Log($"Unload view: {uiName}");
        }
        else
        {
            Debug.LogErrorFormat("The view({0}) you are trying to unload has not been loaded.", uiName);
        }
    }
}