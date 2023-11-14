using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	private float _currentHealh;
	private float _maxHealth = 10f;
	private float _minHealth = 0f;

	private void Awake()
	{
		_currentHealh = _maxHealth;

		Debug.Log($"Player health: {_currentHealh}");
	}

	public void TakeDamage(float damage)
	{
		_currentHealh -= damage;

		Debug.Log($"Player health: {_currentHealh}");

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

		Debug.Log("Player died");

		SceneManager.LoadScene(SceneMain);
	}
}
