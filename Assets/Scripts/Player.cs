using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	[HideInInspector] public float CurrentHealh { get; private set; }
	[HideInInspector] public float MaxHealth { get; private set; } = 10f;
	[HideInInspector] public float MinHealth { get; private set; } = 0f;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();

		CurrentHealh = MaxHealth;
	}

	public void TakeDamage(float damage)
	{
		const string ParameterHit = "Hit";

		_animator.SetTrigger(ParameterHit);
		CurrentHealh -= damage;

		CheckHealth();
		DisplayCurrentHealth();
	}

	public void TakeHeal(float heal)
	{
		const string ParameterHeal = "Heal";

		_animator.SetTrigger(ParameterHeal);
		CurrentHealh += heal;

		CheckHealth();
		DisplayCurrentHealth();
	}

	private void CheckHealth()
	{
		if (CurrentHealh <= MinHealth)
		{
			Die();
		}
		else if (CurrentHealh > MaxHealth)
		{
			CurrentHealh = MaxHealth;
		}
	}

	private void Die()
	{
		const string SceneMain = "MainScene";

		SceneManager.LoadScene(SceneMain);
	}

	private void DisplayCurrentHealth()
	{
		Debug.Log($"Player current health: {CurrentHealh}");
	}
}
