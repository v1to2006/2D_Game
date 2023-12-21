using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private const string ButtonFire1 = "Fire1";

    [SerializeField] private PlayerMesh _playerMesh;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackDelayTime;
    [SerializeField] private LayerMask _enemyLayer;

    private Coroutine _tryAttackEnemyCoroutine;

    private void Start()
    {
        _tryAttackEnemyCoroutine = StartCoroutine(TryAttackEnemy());
    }

    private IEnumerator TryAttackEnemy()
    {
        WaitForSeconds attackDelay = new WaitForSeconds(_attackDelayTime);

        while (true)
        {
            if (Input.GetButtonDown(ButtonFire1))
            {
                _playerMesh.PlayerAnimator.SetAttack();

                Collider2D hit = Physics2D.OverlapCircle(_attackPoint.position, _attackRadius, _enemyLayer);

                if (hit != null)
                {
                    if (hit.transform.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        AttackEnemy(enemy);
                    }
                }

                yield return attackDelay;
            }

            yield return null;
        }
    }

    private void AttackEnemy(Enemy enemy)
    {
        enemy.TakeDamage(_damage);
    }

    private void OnDestroy()
    {
        StopCoroutine(_tryAttackEnemyCoroutine);
    }
}
