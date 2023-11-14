using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	private Animator _animator;
	private Rigidbody2D _rigidbody2D;

	private float _currentHealh;
	private float _maxHealth = 10f;
	private float _minHealth = 0f;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_rigidbody2D = GetComponent<Rigidbody2D>();

		_currentHealh = _maxHealth;
	}

	public void TakeDamage(float damage)
	{
		const string ParameterHit = "Hit";

		_animator.SetTrigger(ParameterHit);
		_currentHealh -= damage;

		DisplayCurrentHealth();

		CheckHealth();
	}

	private void CheckHealth()
	{
		if (_currentHealh <= _minHealth)
		{
			Die();
		}
	}

	private void Die()
	{
		const string SceneMain = "MainScene";

		SceneManager.LoadScene(SceneMain);
	}

	private void DisplayCurrentHealth()
	{
		Debug.Log($"Player health: {_currentHealh}");
	}
}
