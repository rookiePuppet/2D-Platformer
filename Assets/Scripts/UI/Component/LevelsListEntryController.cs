using UnityEngine.UIElements;

public class LevelsListEntryController
{
    private Label _levelNameLabel;
    private VisualElement _levelPreviewImage;

    public void SetVisualElement(VisualElement visualElement)
    {
        _levelNameLabel = visualElement.Q<Label>("LevelNameLabel");
        _levelPreviewImage = visualElement.Q<VisualElement>("LevelPreviewImage");
    }

    public void SetLevelData(GameLevelData levelData)
    {
        _levelNameLabel.text = levelData.levelName;
        _levelPreviewImage.style.backgroundImage = new StyleBackground(levelData.previewImage);
    }
}