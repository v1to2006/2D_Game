using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 5f;
    
    private EnemyAnimator _enemyAnimator;
    private float _minHealth = 0f;
    private float _currentHealth;

    private void Awake()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _enemyAnimator.SetTakeDamage();
        _currentHealth -= damage;

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
