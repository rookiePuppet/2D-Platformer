using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIBase : MonoBehaviour
{
    protected UIDocument uiDocument;
    protected VisualElement Root => uiDocument.rootVisualElement;
    protected abstract int SortingOrder { get; set; }

    protected virtual void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        uiDocument.sortingOrder = SortingOrder;
    }

    public abstract void Show();
    public abstract void Hide();
}