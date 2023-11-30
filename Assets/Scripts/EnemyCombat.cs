using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
	[SerializeField] private float _attackDamage;
	[SerializeField] private float _attackCooldownTime;

	private Player _player = null;

	private void Awake()
	{
		StartCoroutine(AttackPlayer(_player));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Player>(out Player player))
		{
			_player = player;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Player>(out Player player))
		{
			_player = null;
		}
	}

	private IEnumerator AttackPlayer(Player player)
	{
		WaitForSeconds cooldown = new WaitForSeconds(_attackCooldownTime);

		while (true)
		{
			if (_player != null)
			{
				player.TakeDamage(_attackDamage);

				yield return cooldown;
			}

			yield return null;
		}
	}
}
