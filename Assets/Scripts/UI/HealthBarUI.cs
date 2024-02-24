using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image barForeground;
    
    public void SetHealthBar(float health, float maxHealth)
    {
        barForeground.fillAmount = health / maxHealth;
    }
}