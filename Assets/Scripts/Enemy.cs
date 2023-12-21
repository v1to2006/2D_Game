using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour
{
    public static event Action<Enemy, float, float> HealthChanged;

    [SerializeField] private EnemyMesh _enemyMesh;
    [SerializeField] private float _maxHealth = 5f;
    
    private float _minHealth = 0f;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;

        HealthChanged?.Invoke(this, _currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _enemyMesh.EnemyAnimator.SetTakeDamage();
        _currentHealth -= damage;

        HealthChanged?.Invoke(this, _currentHealth, _maxHealth);
        CheckHealth();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void CheckHealth()
    {
        if (_currentHealth <= _minHealth)
        {
            Die();
        }
    }
}
