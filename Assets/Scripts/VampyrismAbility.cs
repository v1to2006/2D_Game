using System;
using System.Collections;
using UnityEngine;

public class VampyrismAbility : MonoBehaviour
{
    public static event Action<float> HealthVampyrised;

    [SerializeField] private KeyCode _abilityKey;
    [SerializeField] private float _abilityRadius;
    [SerializeField] private float _abilityDuration;
    [SerializeField] private float _abilityDamage;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Color _lineColor;


    private Coroutine _tryVampyriseCoroutine;
    private Coroutine _vampyriseCoroutine;

    private void Start()
    {
        _lineRenderer.material.color = _lineColor;

        _tryVampyriseCoroutine = StartCoroutine(TryVampyrise());
    }

    private IEnumerator TryVampyrise()
    {
        WaitForSeconds cooldown = new WaitForSeconds(_cooldownTime);

        while (true)
        {
            if (Input.GetKeyDown(_abilityKey))
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _abilityRadius, _enemyLayer);

                Enemy closestEnemy = GetClosestEnemy(hits);

                if (closestEnemy != null)
                {
                    _vampyriseCoroutine = StartCoroutine(Vampyrise(closestEnemy, hits, () =>
                    {
                        SetLinePosition(new Vector2(0, 0), new Vector2(0, 0));
                    }));

                    yield return cooldown;
                }
            }

            yield return null;
        }
    }

    private IEnumerator Vampyrise(Enemy enemy, Collider2D[] hits, Action complete)
    {
        float duration = _abilityDuration;

        while (duration > 0 && enemy != null && EnemyInRange(enemy, hits))
        {
            hits = Physics2D.OverlapCircleAll(transform.position, _abilityRadius, _enemyLayer);

            SetLinePosition(transform.position, enemy.transform.position);

            enemy.TakeDamage(_abilityDamage);
            HealthVampyrised?.Invoke(_abilityDamage);

            duration -= Time.deltaTime;

            yield return null;
        }

        complete?.Invoke();
    }

    private bool EnemyInRange(Enemy targetEnemy, Collider2D[] hits)
    {
        if (hits == null)
            return false;

        foreach (Collider2D hit in hits)
        {
            if (hit.transform.TryGetComponent<Enemy>(out Enemy enemy) == targetEnemy)
            {
                return true;
            }
        }

        return false;
    }

    private Enemy GetClosestEnemy(Collider2D[] hits)
    {
        Enemy closestEnemy = null;

        foreach (Collider2D hit in hits)
        {
            if (hit.transform.TryGetComponent<Enemy>(out Enemy enemy))
            {
                float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);

                if (closestEnemy == null)
                {
                    closestEnemy = enemy;
                }
                else if (enemyDistance < Vector3.Distance(transform.position, closestEnemy.transform.position))
                {
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }

    private void SetLinePosition(Vector2 current, Vector2 target)
    {
        _lineRenderer.SetPosition(0, current);
        _lineRenderer.SetPosition(1, target);
    }

    private void OnDestroy()
    {
        if (_vampyriseCoroutine != null)
            StopCoroutine(_vampyriseCoroutine);

        if (_tryVampyriseCoroutine != null)
            StopCoroutine(_tryVampyriseCoroutine);
    }
}
