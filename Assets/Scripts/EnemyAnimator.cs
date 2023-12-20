using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private readonly int TakeDamage = Animator.StringToHash(nameof(TakeDamage));
    private readonly int Die = Animator.StringToHash(nameof(Die));

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetTakeDamage()
    {
        _animator.SetTrigger(TakeDamage);
    }

    public void SetDie()
    {
        _animator.SetTrigger(Die);
    }
}
