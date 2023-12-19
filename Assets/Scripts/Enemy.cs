using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 5f;
    
    private float _minHealth = 0f;
    private float _currentHealth;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        const string ParameterHit = "Hit";

        _animator.SetTrigger(ParameterHit);
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
