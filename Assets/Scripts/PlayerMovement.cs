using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private const string AxisHorizontal = "Horizontal";
    private const string ButtonJump = "Jump";

    private readonly Quaternion RightRotation = Quaternion.Euler(0, 0, 0);
    private readonly Quaternion LeftRotation = Quaternion.Euler(0, 180, 0);

    [SerializeField] private PlayerMesh _playerMesh;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody2d;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private bool IsGrounded()
    {
        float radius = 0.3f;

        return Physics2D.OverlapCircle(_groundCheck.position, radius, _groundLayer);
    }

    private void Jump()
    {
        if (Input.GetButtonDown(ButtonJump) && IsGrounded())
        {
            _playerMesh.PlayerAnimator.SetJump();
            _rigidbody2d.AddForce(Vector2.up * _jumpForce);
        }

        _playerMesh.PlayerAnimator.SetFall(!IsGrounded());
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxisRaw(AxisHorizontal);
        _playerMesh.PlayerAnimator.SetRun(Mathf.Abs(horizontalMove));

        _rigidbody2d.velocity = new Vector2(horizontalMove * _movementSpeed * Time.fixedDeltaTime, _rigidbody2d.velocity.y);
        Flip(horizontalMove);
    }

    private void Flip(float horizontalMove)
    {
        if (horizontalMove < 0)
        {
            _playerMesh.transform.localRotation = LeftRotation;
        }

        if (horizontalMove > 0)
        {
            _playerMesh.transform.localRotation = RightRotation;
        }
    }
}
