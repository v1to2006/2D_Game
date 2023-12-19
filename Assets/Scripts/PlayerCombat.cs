using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackDelayTime;
    [SerializeField] private LayerMask _enemyLayer;

    private Animator _animator;

    private Coroutine _tryAttackEnemyCoroutine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _tryAttackEnemyCoroutine = StartCoroutine(TryAttackEnemy());
    }

    private IEnumerator TryAttackEnemy()
    {
        const string ButtonFire1 = "Fire1";
        const string AnimationParameterAttack = "Attack";

        WaitForSeconds attackDelay = new WaitForSeconds(_attackDelayTime);

        while (true)
        {
            if (Input.GetButtonDown(ButtonFire1))
            {
                _animator.SetTrigger(AnimationParameterAttack);

                Collider2D hit = Physics2D.OverlapCircle(_attackPoint.position, _attackRadius, _enemyLayer);

                if (hit)
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
