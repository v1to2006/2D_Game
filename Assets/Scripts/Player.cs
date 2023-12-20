using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    private const string SceneMain = "MainScene";

    private PlayerAnimator _playerAnimator;
    private float _currentHealth;
    private float _maxHealth = 10f;
    private float _minHealth = 0f;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();

        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _playerAnimator.SetTakeDamage();
        _currentHealth -= damage;

        CheckHealth();
    }

    public void TakeHeal(float heal)
    {
        _playerAnimator.SetHeal();
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
        SceneManager.LoadScene(SceneMain);
    }
}
