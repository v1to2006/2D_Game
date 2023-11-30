using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private Transform _movementPointsContainer;
	[SerializeField] private float _movementSpeed;
	[SerializeField] private float _reachDistance = 0.1f;

	private Rigidbody2D _rigidbody2d;
	private Transform[] _movementPoints;
	private int _currentPoint;

	private void Awake()
	{
		_rigidbody2d = GetComponent<Rigidbody2D>();
		_movementPoints = new Transform[_movementPointsContainer.childCount];

		for (int i = 0; i < _movementPointsContainer.childCount; i++)
		{
			_movementPoints[i] = _movementPointsContainer.GetChild(i);
		}
	}

	private void FixedUpdate()
	{
		Patrol();
	}

	private void Patrol()
	{
		Transform target = _movementPoints[_currentPoint];
		transform.position = Vector2.MoveTowards(transform.position, target.position, _movementSpeed * Time.deltaTime);
		Flip();

		if (Vector2.Distance(transform.position, target.position) <= _reachDistance)
		{
			_currentPoint++;

			if (_currentPoint >= _movementPoints.Length)
			{
				_currentPoint = 0;
			}
		}
	}

	private void Chase()
	{

	}

	private void Flip()
	{
		if (transform.position.x < _movementPoints[_currentPoint].position.x)
		{
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
		else if (transform.position.x > _movementPoints[_currentPoint].position.x)
		{
			transform.localRotation = Quaternion.Euler(0, -180, 0);
		}
	}
}
