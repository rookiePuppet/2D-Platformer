using UnityEngine;
using UnityEngine.UIElements;

public class MainController : UIBase
{
    [SerializeField] private PlayerStatsSO playerStats;

    private WeaponsHolder _weaponsHolder;

    private VisualElement Root => _uiDoc.rootVisualElement;

    private UIDocument _uiDoc;
    private VisualElement _healthBarForeground;
    private Label _primaryWeaponLabel;
    private Label _secondaryWeaponLabel;

    private void Awake()
    {
        _uiDoc = GetComponent<UIDocument>();
        _weaponsHolder = FindAnyObjectByType<WeaponsHolder>();
    }

    private void OnEnable()
    {
        _healthBarForeground = Root.Q("HealthBar").Q("Foreground");
        _primaryWeaponLabel = Root.Q("PrimaryWeaponIcon").Q<Label>();
        _secondaryWeaponLabel = Root.Q("SecondaryWeaponIcon").Q<Label>();

        playerStats.HealthChanged += OnHealthChanged;
        _weaponsHolder.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        playerStats.HealthChanged -= OnHealthChanged;
        _weaponsHolder.WeaponChanged -= OnWeaponChanged;
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        var ratio = currentHealth / maxHealth;
        _healthBarForeground.style.width = new StyleLength(Length.Percent(ratio * 100f));
    }

    private void OnWeaponChanged(Weapon primaryWeapon, Weapon secondaryWeapon)
    {
        _primaryWeaponLabel.text = primaryWeapon ? primaryWeapon.WeaponData.weaponName : "空";
        _secondaryWeaponLabel.text = secondaryWeapon ? secondaryWeapon.WeaponData.weaponName : "空";
    }
}