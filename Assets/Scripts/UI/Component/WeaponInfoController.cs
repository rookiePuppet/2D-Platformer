using System;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponInfoController
{
    public WeaponDataSO WeaponData { get; private set; }

    private VisualElement _frameElement;
    private VisualElement _weaponIconElement;
    private Label _weaponNameLabel;
    private Label _weaponTypeLabel;
    private Label _weaponPropertyLabel;

    private event Action<WeaponInfoController> SelectedWeaponChanged;

    public void SetVisualElement(VisualElement visualElement)
    {
        _frameElement = visualElement;
        _weaponIconElement = visualElement.Q<VisualElement>("WeaponIcon");
        _weaponNameLabel = visualElement.Q<Label>("WeaponName");
        _weaponTypeLabel = visualElement.Q<Label>("WeaponType");
        _weaponPropertyLabel = visualElement.Q<Label>("WeaponProperty");
    }

    public void SetWeaponInfo(WeaponDataSO weaponData)
    {
        WeaponData = weaponData;

        _weaponIconElement.style.backgroundImage = new StyleBackground(weaponData.itemSprite);
        _weaponNameLabel.text = weaponData.itemName;
        _weaponTypeLabel.text = GetWeaponTypeName(weaponData.weaponType);
        _weaponPropertyLabel.text = GetWeaponPropertyInfo(weaponData);
    }

    public void RegisterFrameClickedEvent(Action<WeaponInfoController> selectedWeaponChanged)
    {
        SelectedWeaponChanged = selectedWeaponChanged;

        _frameElement.RegisterCallback<ClickEvent>(_ =>
        {
            SelectedWeaponChanged?.Invoke(this);
        });
    }

    public void UnregisterFrameClickedEvent(Action<WeaponInfoController> selectedWeaponChanged)
    {
        _frameElement.UnregisterCallback<ClickEvent>(_ => { SelectedWeaponChanged?.Invoke(this); });
    }

    public void SelectWeapon()
    {
        _frameElement.style.borderBottomWidth = new StyleFloat(10f);
    }

    public void UnselectWeapon()
    {
        _frameElement.style.borderBottomWidth = new StyleFloat(0f);
    }

    private string GetWeaponPropertyInfo(WeaponDataSO weaponData)
    {
        switch (weaponData)
        {
            case AggressiveWeaponDataSO:
            {
                var builder = new StringBuilder("伤害 ");

                switch (weaponData)
                {
                    case MeleeWeaponDataSO meleeWeaponData:
                    {
                        foreach (var attackDetails in meleeWeaponData.WeaponDetails)
                        {
                            builder.Append($"{attackDetails.damageAmount};");
                        }

                        break;
                    }
                    case RangedWeaponDataSO rangedWeaponData:
                        builder.Append($"{rangedWeaponData.damageAmount}");
                        break;
                }

                return builder.ToString();
            }
            case DefensiveWeaponDataSO defensiveWeaponData:
                return $"减伤率 {defensiveWeaponData.damageReductionRate * 100f}%";
            default:
                return string.Empty;
        }
    }

    private string GetWeaponTypeName(WeaponType weaponType) => weaponType switch
    {
        WeaponType.Aggressive => "攻击",
        WeaponType.Defensive => "防御",
        _ => "未定义"
    };
}