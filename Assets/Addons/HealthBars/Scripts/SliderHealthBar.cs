using UnityEngine;
using UnityEngine.UI;

public class SliderHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _sliderDisplay;
    [SerializeField] private Image _fillRectImage;
    [SerializeField] private Color _fillColor;
    [SerializeField] private Color _emptyColor;

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
        if (currentHealth <= 0)
        {
            _fillRectImage.color = _emptyColor;
        }
        else
        {
            _fillRectImage.color = _fillColor;
        }

        _sliderDisplay.minValue = 0;
        _sliderDisplay.maxValue = maxHealth;

        _sliderDisplay.value = currentHealth;
    }
}
