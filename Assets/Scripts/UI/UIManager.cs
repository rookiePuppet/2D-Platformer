using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "UIManager", menuName = "Tool/UIManager")]
public class UIManager : ScriptableObject
{
    [SerializeField] private string rootPath = "UI";

    private readonly Dictionary<string, IUIBehaviour> _uis = new();

    public TUI LoadUI<TUI>(Action uiLoaded = null) where TUI : class, IUIBehaviour
    {
        var uiName = typeof(TUI).Name;

        if (_uis.TryGetValue(uiName, out var ui))
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

            ui = gameObject.GetComponent<TUI>();
            _uis[uiName] = ui;

            ui.Show();
        }

        uiLoaded?.Invoke();

        return ui as TUI;
    }

    public void UnloadUI<TUI>() where TUI : class, IUIBehaviour
    {
        var uiName = typeof(TUI).Name;

        if (_uis.TryGetValue(uiName, out var ui))
        {
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

        string path;

        if (typeof(View).IsAssignableFrom(uiType))
        {
            path = $"{rootPath}/View/{uiType.Name}";
        }
        else if (typeof(Window).IsAssignableFrom(uiType))
        {
            path = $"{rootPath}/Window/{uiType.Name}";
        }
        else if(typeof(Dialog).IsAssignableFrom(uiType))
        {
            path = $"{rootPath}/Dialog/{uiType.Name}";
        }
        else
        {
            path = $"{rootPath}/UGUI/{uiType.Name}";
        }

        return path;
    }
}