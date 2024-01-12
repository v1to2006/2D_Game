using UnityEngine;
using UnityEngine.Events;

public class Objective : MonoBehaviour
{
	public event UnityAction EnemiesEliminated;

	[SerializeField] private EnemyElimination _enemyElimination;

	private int _enemiesLeft;

	private void Start()
	{
		_enemiesLeft = FindObjectsOfType<Enemy>().Length;
	}

	private void OnEnable()
	{
		_enemyElimination.EnemyEliminated += EnemyEliminated;
	}

	private void OnDisable()
	{
		_enemyElimination.EnemyEliminated -= EnemyEliminated;
	}

	private void CheckEnemiesLeft()
	{
		if (_enemiesLeft <= 0)
		{
			EnemiesEliminated?.Invoke();
		}
	}

	private void EnemyEliminated()
	{
		_enemiesLeft--;

		CheckEnemiesLeft();
	}
}
