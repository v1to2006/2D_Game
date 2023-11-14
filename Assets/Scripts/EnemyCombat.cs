using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
	private float _attackDamage = 2f;
	private float _attackCooldownTime = 1f;
	private bool _isCooldown = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Player>(out Player player))
		{
			while (true)
			{
				if (!_isCooldown)
				{
					AttackPlayer(player);
					StartCooldown();
				}
			}
		}
	}

	private void AttackPlayer(Player player)
	{
		player.TakeDamage(_attackDamage);
	}

	private void StartCooldown()
	{
		_isCooldown = true;
		Invoke(nameof(ResetCooldown), _attackCooldownTime);
	}

	private void ResetCooldown()
	{
		_isCooldown = false;
	}
}
