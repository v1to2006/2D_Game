using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [HideInInspector] public float CurrentHealth { get; private set; }
    [HideInInspector] public float MaxHealth { get; private set; } = 10f;

    private float _minHealth = 0f;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        const string ParameterHit = "Hit";

        _animator.SetTrigger(ParameterHit);
        CurrentHealth -= damage;

        CheckHealth();
    }

    public void TakeHeal(float heal)
    {
        const string ParameterHeal = "Heal";

        _animator.SetTrigger(ParameterHeal);
        CurrentHealth += heal;

        CheckHealth();
    }

    private void CheckHealth()
    {
        if (CurrentHealth <= _minHealth)
        {
            Die();
        }
        else if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    private void Die()
    {
        const string SceneMain = "MainScene";

        SceneManager.LoadScene(SceneMain);
    }
}
