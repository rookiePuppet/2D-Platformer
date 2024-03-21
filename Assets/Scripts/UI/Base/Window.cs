using System;
using UnityEngine.UIElements;

public class Window : UIBase
{
    protected override int SortingOrder { get; set; } = 1;
    
    private Button _closeButton;
    public event Action Closed;

    protected virtual void OnEnable()
    {
        _closeButton = Root.Q<Button>("CloseButton");

        _closeButton.clicked += Hide;
    }

    protected virtual void OnDisable()
    {
        _closeButton.clicked -= Hide;
    }


    public override void Show()
    {
        gameObject.SetActive(true);
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
        Closed?.Invoke();
    }
}