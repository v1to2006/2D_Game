using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static event Action<float, float> HealthChanged;

    private const string SceneMain = "MainScene";

    [SerializeField] private PlayerMesh _playerMesh;

    private float _currentHealth;
    private float _maxHealth = 10f;
    private float _minHealth = 0f;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _playerMesh.PlayerAnimator.SetTakeDamage();
        _currentHealth -= damage;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
        CheckHealth();
    }

    public void TakeHeal(float heal)
    {
        _playerMesh.PlayerAnimator.SetHeal();
        _currentHealth += heal;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
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
