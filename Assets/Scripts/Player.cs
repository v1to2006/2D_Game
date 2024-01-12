using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public event UnityAction<float, float> HealthChanged;

    [SerializeField] private PlayerMesh _playerMesh;
    [SerializeField] private VampyrismAbility _vampyrismAbility;
    [SerializeField] private string _mainMenuScene;

    private float _currentHealth;
    private float _maxHealth = 10f;
    private float _minHealth = 0f;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	private void Start()
    {
        _currentHealth = _maxHealth;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void OnEnable()
    {
        _vampyrismAbility.HealthVampyrised += TakeHeal;
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
        if (_currentHealth == _maxHealth)
            return;

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
        SceneManager.LoadScene(_mainMenuScene);
    }
}
