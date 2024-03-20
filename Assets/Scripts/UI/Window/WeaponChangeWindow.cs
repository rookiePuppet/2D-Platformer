using System;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponChangeWindow : Window
{
    [SerializeField] private InventoryManager inventoryManager;

    private VisualElement _primaryWeaponContainer;
    private VisualElement _secondaryWeaponContainer;
    private VisualElement _alternativeWeaponContainer;

    private Button _confirmButton;

    private readonly WeaponInfoController _primaryController = new();
    private readonly WeaponInfoController _secondaryController = new();
    private readonly WeaponInfoController _alternativeController = new();

    private event Action<WeaponInfoController> SelectedWeaponChanged;
    private WeaponInfoController _selectedWeaponController;

    private WeaponDataSO _alternativeWeaponData;
    private Item _alternativeWeaponItem;

    protected override void OnEnable()
    {
        base.OnEnable();

        _primaryWeaponContainer = Root.Q("Primary");
        _secondaryWeaponContainer = Root.Q("Secondary");
        _alternativeWeaponContainer = Root.Q("AlternativeWeapon");
        _confirmButton = Root.Q<Button>("ConfirmButton");

        _primaryWeaponContainer.userData = _primaryController;
        _secondaryWeaponContainer.userData = _secondaryController;
        _alternativeWeaponContainer.userData = _alternativeController;

        _primaryController.SetVisualElement(_primaryWeaponContainer);
        _secondaryController.SetVisualElement(_secondaryWeaponContainer);
        _alternativeController.SetVisualElement(_alternativeWeaponContainer);

        if (inventoryManager.IsPrimaryWeaponExists)
        {
            _primaryController.SetWeaponInfo(inventoryManager.PrimaryWeapon.WeaponData);
        }

        if (inventoryManager.IsSecondaryWeaponExists)
        {
            _secondaryController.SetWeaponInfo(inventoryManager.SecondaryWeapon.WeaponData);
        }
        
        SelectedWeaponChanged = OnSelectedWeaponChanged;
        
        _primaryController.RegisterFrameClickedEvent(SelectedWeaponChanged);
        _secondaryController.RegisterFrameClickedEvent(SelectedWeaponChanged);

        _confirmButton.clicked += OnConfirmButtonClicked;

        _selectedWeaponController = null;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _primaryController.UnregisterFrameClickedEvent(SelectedWeaponChanged);
        _secondaryController.UnregisterFrameClickedEvent(SelectedWeaponChanged);
    }

    private void OnSelectedWeaponChanged(WeaponInfoController selectedWeapon)
    {
        if (_selectedWeaponController == selectedWeapon) return;

        _selectedWeaponController = selectedWeapon;
        if (selectedWeapon == _primaryController)
        {
            _primaryController.SelectWeapon();
            _secondaryController.UnselectWeapon();
        }
        else
        {
            _secondaryController.SelectWeapon();
            _primaryController.UnselectWeapon();
        }
    }

    public void SetAlternativeWeapon(Item weaponItem)
    {
        _alternativeWeaponItem = weaponItem;
        
        var weaponData = weaponItem.Data as WeaponDataSO;
        _alternativeWeaponData = weaponData;
        _alternativeController.SetWeaponInfo(weaponData);
    }

    private void OnConfirmButtonClicked()
    {
        if (_selectedWeaponController is null)
        {
            Debug.Log("请选择武器替换位置!");
            return;
        }

        var weaponOrder = _selectedWeaponController == _primaryController
            ? CombatInputs.Primary
            : CombatInputs.Secondary;

        inventoryManager.ChangeWeapon(_alternativeWeaponData, weaponOrder);
        Destroy(_alternativeWeaponItem.gameObject);
        
        Hide();
    }
}