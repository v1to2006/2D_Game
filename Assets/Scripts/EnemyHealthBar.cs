using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Enemy _enemyParent;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fillRectImage;
    [SerializeField] private Color _fillColor;
    [SerializeField] private Color _emptyColor;
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private Enemy _enemy;

    private Coroutine _healthTransitionCoroutine = null;

    private void OnEnable()
    {
        _enemy.HealthChanged += DisplayHealth;
    }

    private void DisplayHealth(Enemy enemy, float currentHealth, float maxHealth)
    {
        if (_enemyParent != enemy)
            return;

        _slider.minValue = 0;
        _slider.maxValue = maxHealth;

        if (_healthTransitionCoroutine != null)
            StopCoroutine(_healthTransitionCoroutine);

        _healthTransitionCoroutine = StartCoroutine(TransitionHealth(currentHealth));
    }

    private IEnumerator TransitionHealth(float currentHealth)
    {
        while (_slider != null && _slider.value != currentHealth)
        {
            if (currentHealth <= 0)
            {
                if (_fillRectImage != null)
                {
                    _fillRectImage.color = _emptyColor;
                }
            }
            else
            {
                if (_fillRectImage != null)
                {
                    _fillRectImage.color = _fillColor;
                }
            }

            if (_slider != null)
            {
                _slider.value = Mathf.Lerp(_slider.value, currentHealth, _transitionSpeed * Time.deltaTime);
            }

            yield return null;
        }
    }
}
