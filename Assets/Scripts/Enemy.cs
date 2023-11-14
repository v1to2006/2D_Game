using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void Die() 
	{
		const string ParameterDie = "Die";

		_animator.SetTrigger(ParameterDie);
	}

	public void DestroySelf()
	{
		Destroy(gameObject);
	}
}
