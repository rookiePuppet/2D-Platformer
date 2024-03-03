using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    private VisualElement _root;
    private VisualElement _healthBar;
    private VisualElement _healthBarForeground;

    [SerializeField] private PlayerStatsSO playerStats;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _healthBar = _root.Q("HealthBar");
        _healthBarForeground = _healthBar.Q("Foreground");
    }

    private void OnEnable()
    {
        playerStats.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        playerStats.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        UpdateHealthBar(playerStats.Health, playerStats.MaxHealth);
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        var ratio = currentHealth / maxHealth;
        _healthBarForeground.style.width = new StyleLength(Length.Percent(ratio * 100f));
    }
}
