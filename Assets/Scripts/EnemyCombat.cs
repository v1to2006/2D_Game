using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private float _attackDamage = 2f;
    private float _attackCooldownTime = 1f;

    Coroutine _attackCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _attackCoroutine = StartCoroutine(AttackPlayer(player));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            StopCoroutine(_attackCoroutine);
        }
    }

    private IEnumerator AttackPlayer(Player player)
    {
        WaitForSeconds cooldown = new WaitForSeconds(_attackCooldownTime);

        while (true)
        {
            player.TakeDamage(_attackDamage);

            yield return cooldown;
        }
    }
}
