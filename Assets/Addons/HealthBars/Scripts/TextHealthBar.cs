using TMPro;
using UnityEngine;

public class TextHealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDisplay;

    private void OnEnable()
    {
        //Health.HealthChanged += DisplayHealth;
    }

    private void OnDisable()
    {
        //Health.HealthChanged -= DisplayHealth;
    }

    private void DisplayHealth(float currentHealth, float maxHealth)
    {
        string displayText = $"{currentHealth}/{maxHealth}";

        _textDisplay.text = displayText;
    }
}