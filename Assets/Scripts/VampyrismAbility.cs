using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VampyrismAbility : MonoBehaviour
{
    public UnityAction<float> HealthVampyrised;

    [SerializeField] private KeyCode _abilityKey;
    [SerializeField] private float _abilityRadius;
    [SerializeField] private float _abilityDuration;
    [SerializeField] private float _abilityDamage;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Color _lineColor;

    private Vector2 _defaultLineRendererPosition = new Vector2(0, 0);

    private void Start()
    {
        _lineRenderer.material.color = _lineColor;

        StartCoroutine(TryVampyrise());
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
                    StartCoroutine(Vampyrise(closestEnemy, hits, () =>
                    {
                        UpdateLineRendererPosition(_defaultLineRendererPosition, _defaultLineRendererPosition);
                    }));

                    yield return cooldown;
                }
            }

            yield return null;
        }
    }

    private IEnumerator Vampyrise(Enemy enemy, Collider2D[] hits, UnityAction complete)
    {
        float duration = _abilityDuration;

        while (duration > 0 && enemy != null && EnemyInRange(enemy, hits))
        {
            hits = Physics2D.OverlapCircleAll(transform.position, _abilityRadius, _enemyLayer);

            UpdateLineRendererPosition(transform.position, enemy.transform.position);

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

    private void UpdateLineRendererPosition(Vector2 startPoint, Vector2 endPoint)
    {
        _lineRenderer.SetPosition(0, startPoint);
        _lineRenderer.SetPosition(1, endPoint);
    }
}
