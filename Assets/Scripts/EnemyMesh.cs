using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyMesh : MonoBehaviour
{
    private EnemyAnimator _enemyAnimator;

    public EnemyAnimator EnemyAnimator => _enemyAnimator;

    private void Awake()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }
}
