using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private readonly int Speed = Animator.StringToHash(nameof(Speed));
    private readonly int Jump = Animator.StringToHash(nameof(Jump));
    private readonly int Fall = Animator.StringToHash(nameof(Fall));
    private readonly int Attack = Animator.StringToHash(nameof(Attack));
    private readonly int TakeDamage = Animator.StringToHash(nameof(TakeDamage));
    private readonly int Heal = Animator.StringToHash(nameof(Heal));

    [SerializeField] private Animator _animator;

    public void SetJump()
    {
        _animator.SetTrigger(Jump);
    }

    public void SetFall(bool isGrounded)
    {
        _animator.SetBool(Fall, isGrounded);
    }

    public void SetRun(float move)
    {
        _animator.SetFloat(Speed, move);
    }

    public void SetAttack()
    {
        _animator.SetTrigger(Attack);
    }

    public void SetTakeDamage()
    {
        _animator.SetTrigger(TakeDamage);
    }

    public void SetHeal()
    {
        _animator.SetTrigger(Heal);
    }
}
