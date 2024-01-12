using UnityEngine;
using UnityEngine.Events;

public class EnemyElimination : MonoBehaviour
{
    public event UnityAction EnemyEliminated;

    public void EnemyDied()
    {
        EnemyEliminated?.Invoke();
    }
}
