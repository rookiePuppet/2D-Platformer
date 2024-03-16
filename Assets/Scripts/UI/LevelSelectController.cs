using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelectController : UIBase
{
    [Header("Components")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SceneLoaderSO sceneLoader;

    [Header("UI")]
    [SerializeField] private GameLevelsDataSO levelsData;
    [SerializeField] private float mouseWheelSpeed = 100f;

    private UIDocument _uiDoc;
    private VisualElement Root => _uiDoc.rootVisualElement;

    private ListView _levelsList;

    private void Awake()
    {
        _uiDoc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _levelsList = Root.Q<ListView>("LevelsList");

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


        _levelsList.selectionChanged += (items) =>
        {
            var data = _levelsList.selectedItem is GameLevelData item ? item : default;
            print(data.levelName);
        };

        _levelsList.itemsSource = levelsData.data;
        _levelsList.SetSelection(0);
        
        Root.Q<Button>("ReturnButton").clicked += OnReturnButtonClicked;
        Root.Q<Button>("ConfirmButton").clicked += OnConfirmButtonClicked;
    }

    private void OnReturnButtonClicked()
    {
        uiManager.UnloadView(View.LevelSelectView);
    }

    private async void OnConfirmButtonClicked()
    {
        var level = levelsData.data[_levelsList.selectedIndex];
        await sceneLoader.LoadSceneAsync(level.scene.name);
    }
}