using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Enemy _template;
    [SerializeField] private Transform _movementPointsContainer;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _reachDistance = 0.1f;

    private Rigidbody2D _rigidbody2d;
    private Transform[] _movementPoints;
    private int _currentPoint;

    private bool _isChasing = false;

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
        if (_isChasing)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        Transform target = _movementPoints[_currentPoint];
        _template.transform.position = Vector2.MoveTowards(_template.transform.position, target.position, _movementSpeed * Time.deltaTime);
        Flip();

        if (Vector2.Distance(_template.transform.position, target.position) <= _reachDistance)
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
        if (_template.transform.position.x < _movementPoints[_currentPoint].position.x)
        {
            _template.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_template.transform.position.x > _movementPoints[_currentPoint].position.x)
        {
            _template.transform.localRotation = Quaternion.Euler(0, -180, 0);
        }
    }
}
