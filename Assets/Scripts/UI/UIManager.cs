using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "UIManager", menuName = "UIManager")]
public class UIManager : ScriptableObject
{
    [SerializeField] private string rootPath = "UI";

    private readonly Dictionary<string, UIBase> _uis = new();

    public TUI LoadUI<TUI>(Action uiLoaded = null) where TUI : UIBase
    {
        var uiName = typeof(TUI).Name;

        if (_uis.TryGetValue(uiName, out var ui) && ui.gameObject != null)
        {
            ui.Show();
        }
        else
        {
            var original = Resources.Load(GetUIPath<TUI>());
            if (original is null)
            {
                Debug.LogErrorFormat("The UI you are trying to load is not found. Please check the prefab path.");
                return null;
            }

            var gameObject = Instantiate(original);

            ui = gameObject.GetComponent<UIBase>();
            _uis[uiName] = ui;

            ui.Show();
        }

        uiLoaded?.Invoke();

        return ui as TUI;
    }

    public void UnloadUI<TUI>() where TUI : UIBase
    {
        var uiName = typeof(TUI).Name;

        if (_uis.TryGetValue(uiName, out var ui))
        {
            if (ui.gameObject == null)
            {
                Debug.LogErrorFormat("The UI({0}) you are trying to unload is not attached to any game object.",
                    uiName);

                return;
            }

            ui.Hide();

            Debug.Log($"Unload UI: {uiName}");
        }
        else
        {
            Debug.LogErrorFormat("The UI({0}) you are trying to unload has not been loaded.", uiName);
        }
    }

    public void ClearCache()
    {
        _uis.Clear();
    }

    private string GetUIPath<TUI>()
    {
        var uiType = typeof(TUI);
        return typeof(View).IsAssignableFrom(uiType)
            ? $"{rootPath}/View/{uiType.Name}"
            : $"{rootPath}/Dialog/{uiType.Name}";
    }
}