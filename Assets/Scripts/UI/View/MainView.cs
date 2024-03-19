using UnityEngine;
using UnityEngine.UIElements;

public class MainView : View
{
    [SerializeField] private PlayerStatsSO playerStats;

    private WeaponsHolder _weaponsHolder;

    private VisualElement _healthBarForeground;
    private VisualElement _dashEnergyBarForeground;
    private Label _primaryWeaponLabel;
    private Label _secondaryWeaponLabel;

    protected override void Awake()
    {
        base.Awake();

        _weaponsHolder = FindAnyObjectByType<WeaponsHolder>();
    }

    private void OnEnable()
    {
        _healthBarForeground = Root.Q("HealthBar").Q("Foreground");
        _dashEnergyBarForeground = Root.Q("DashEnergyBar").Q("Foreground");
        _primaryWeaponLabel = Root.Q("PrimaryWeaponIcon").Q<Label>();
        _secondaryWeaponLabel = Root.Q("SecondaryWeaponIcon").Q<Label>();

        playerStats.HealthChanged += OnHealthChanged;
        playerStats.DashEnergyChanged += OnDashEnergyChanged;
        _weaponsHolder.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        playerStats.HealthChanged -= OnHealthChanged;
        playerStats.DashEnergyChanged -= OnDashEnergyChanged;
        _weaponsHolder.WeaponChanged -= OnWeaponChanged;
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        var ratio = currentHealth / maxHealth;
        _healthBarForeground.style.width = new StyleLength(Length.Percent(ratio * 100f));
    }

    private void OnDashEnergyChanged(float currentEnergy, float maxEnergy)
    {
        var ratio = currentEnergy / maxEnergy;
        _dashEnergyBarForeground.style.width = new StyleLength(Length.Percent(ratio * 100f));
    }

    private void OnWeaponChanged(Weapon primaryWeapon, Weapon secondaryWeapon)
    {
        _primaryWeaponLabel.text = primaryWeapon ? primaryWeapon.WeaponData.itemName : "空";
        _secondaryWeaponLabel.text = secondaryWeapon ? secondaryWeapon.WeaponData.itemName : "空";
    }
}