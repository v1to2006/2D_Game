using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    private readonly Quaternion _rightRotation = Quaternion.Euler(0, 0, 0);
    private readonly Quaternion _leftRotation = Quaternion.Euler(0, 180, 0);

    [SerializeField] private Transform _movementPointsContainer;
    [SerializeField] private Vector2 _chaseRadius;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _chaseSpeed;
    [SerializeField] private float _reachDistance = 0.1f;

    private Rigidbody2D _rigidbody2d;
    private Vector2 _target;
    private Vector2[] _movementPoints;
    private int _currentPoint;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();

        AddMovementPoints();
    }

    private void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, _chaseRadius, 0f, _playerLayer);

        if (hit != null)
        {
            if (hit.transform.TryGetComponent<Player>(out Player player))
            {
                Chase(player);
            }
        }
        else
        {
            Patrol();
        }
    }

    private void Chase(Player player)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, _chaseSpeed * Time.fixedDeltaTime);

        Flip(player.transform.position);
    }

    private void Patrol()
    {
        _target = _movementPoints[_currentPoint];

        transform.position = Vector2.MoveTowards(transform.position, _target, _movementSpeed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, _target) <= _reachDistance)
        {
            _currentPoint++;

            if (_currentPoint >= _movementPoints.Length)
            {
                _currentPoint = 0;
            }
        }

        Flip(_target);
    }

    private void Flip(Vector2 target)
    {
        if (transform.position.x < target.x)
        {
            transform.localRotation = _rightRotation;
        }
        else if (transform.position.x > target.x)
        {
            transform.localRotation = _leftRotation;
        }
    }

    private void AddMovementPoints()
    {
        _movementPoints = new Vector2[_movementPointsContainer.childCount];

        for (int i = 0; i < _movementPointsContainer.childCount; i++)
        {
            _movementPoints[i] = _movementPointsContainer.GetChild(i).transform.position;
        }

        Destroy(_movementPointsContainer.gameObject);
    }
}
