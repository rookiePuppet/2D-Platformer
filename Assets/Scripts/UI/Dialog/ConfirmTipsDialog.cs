using System;
using UnityEngine.UIElements;

public class ConfirmTipsDialog : Dialog
{
    private Label _titleLabel;
    private Label _contentLabel;

    private Button _confirmButton;
    private Button _cancelButton;

    public event Action Confirmed
    {
        add => _confirmButton.clicked += value;
        remove => _confirmButton.clicked -= value;
    }
    public event Action Canceled
    {
        add => _cancelButton.clicked += value;
        remove => _cancelButton.clicked -= value;
    }

    private void OnEnable()
    {
        _titleLabel = Root.Q<Label>("Title");
        _contentLabel = Root.Q<Label>("Content");
        _confirmButton = Root.Q<Button>("ConfirmButton");
        _cancelButton = Root.Q<Button>("CancelButton");

        _titleLabel.text = "提示";

        Confirmed += Hide;
        Canceled += Hide;
    }

    private void OnDisable()
    {
        Confirmed -= Hide;
        Canceled -= Hide;
    }

    public void SetTitle(string title)
    {
        _titleLabel.text = title;
    }

    public void SetContent(string content)
    {
        _contentLabel.text = content;
    }
}