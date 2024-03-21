using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelectView : View
{
    [Header("Components")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SceneLoader sceneLoader;

    [Header("UI")]
    [SerializeField] private GameLevelsDataSO levelsData;
    [SerializeField] private float mouseWheelSpeed = 100f;

    private ListView _levelsList;

    private Button _returnButton;
    private Button _confirmButton;

    private void OnEnable()
    {
        _levelsList = Root.Q<ListView>("LevelsList");
        _returnButton = Root.Q<Button>("ReturnButton");
        _confirmButton = Root.Q<Button>("ConfirmButton");

        var scrollView = _levelsList.Q<ScrollView>();
        scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
        scrollView.mouseWheelScrollSize = mouseWheelSpeed;

        _levelsList.makeItem += () =>
        {
            var newListEntry = _levelsList.itemTemplate.Instantiate();
            var entryController = new LevelsListEntryController();
            newListEntry.userData = entryController;
            entryController.SetVisualElement(newListEntry);

            return newListEntry;
        };

        _levelsList.bindItem += (item, index) =>
        {
            (item.userData as LevelsListEntryController)?.SetLevelData(levelsData.data[index]);
        };

        Root.RegisterCallback<NavigationMoveEvent>(e =>
        {
            var index = _levelsList.selectedIndex;
            switch (e.direction)
            {
                case NavigationMoveEvent.Direction.Up:
                {
                    index--;
                    if (index < 0) index = levelsData.data.Count - 1;
                    break;
                }
                case NavigationMoveEvent.Direction.Down:
                {
                    index++;
                    if (index >= levelsData.data.Count) index = 0;
                    break;
                }
                case NavigationMoveEvent.Direction.Left:
                    _returnButton.Focus();
                    break;
                case NavigationMoveEvent.Direction.Right:
                    _confirmButton.Focus();
                    break;
            }

            _levelsList.ScrollToItem(index);
            _levelsList.SetSelection(index);

            e.StopPropagation();
        });

        _levelsList.itemsSource = levelsData.data;
        _levelsList.SetSelection(0);

        _levelsList.Focus();

        _returnButton.clicked += OnReturnButtonClicked;
        _confirmButton.clicked += OnConfirmButtonClicked;
    }

    private void OnReturnButtonClicked()
    {
        uiManager.UnloadUI<LevelSelectView>();
        uiManager.LoadUI<StartView>();
    }

    private async void OnConfirmButtonClicked()
    {
        var level = levelsData.data[_levelsList.selectedIndex];
        await sceneLoader.LoadLevelAsync(level);
    }
}