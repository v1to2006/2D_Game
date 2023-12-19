using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private float _currentHealth;
    private float _maxHealth = 10f;
    private float _minHealth = 0f;
    private Animator _animator;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        const string AnimationParameterTakeDamage = "TakeDamage";

        _animator.SetTrigger(AnimationParameterTakeDamage);
        _currentHealth -= damage;

        CheckHealth();
    }

    public void TakeHeal(float heal)
    {
        const string AnimationParameterHeal = "Heal";

        _animator.SetTrigger(AnimationParameterHeal);
        _currentHealth += heal;

        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_currentHealth <= _minHealth)
        {
            Die();
        }
        else if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    private void Die()
    {
        const string SceneMain = "MainScene";

        SceneManager.LoadScene(SceneMain);
    }
}
