using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCombat : MonoBehaviour
{
	[SerializeField] private Transform _attackPoint;
	[SerializeField] private LayerMask _enemyLayer;
	[SerializeField] private float _attackRadius;
	[SerializeField] private float _attackDelayTime;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();

		StartCoroutine(Attack());
	}

	private IEnumerator Attack()
	{
		const string ButtonFire1 = "Fire1";
		const string ParameterAttack = "Attack";

		WaitForSeconds attackDelay = new WaitForSeconds(_attackDelayTime);

		while (true)
		{
			if (Input.GetButtonDown(ButtonFire1))
			{
				_animator.SetTrigger(ParameterAttack);

				Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius, _enemyLayer);

				foreach (Collider2D enemyCollider in detectedEnemies)
				{
					Enemy enemy = enemyCollider.GetComponent<Enemy>();

					KillEnemy(enemy);
				}

				yield return attackDelay;
			}

			yield return null;
		}
	}

	private void KillEnemy(Enemy enemy)
	{
		enemy.Die();
	}
}
