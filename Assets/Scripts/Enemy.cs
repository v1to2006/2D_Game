using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour
{
    public event UnityAction<Enemy, float, float> HealthChanged;

    [SerializeField] private EnemyElimination _enemyElimination;
    [SerializeField] private EnemyMesh _enemyMesh;
    [SerializeField] private float _maxHealth = 5f;
    
    private float _currentHealth;

    public float Health => _currentHealth;

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

        _enemyElimination.EnemyDied();
    }

    private void CheckHealth()
    {
        if (_currentHealth <= 0f)
        {
            Die();
        }
    }
}
