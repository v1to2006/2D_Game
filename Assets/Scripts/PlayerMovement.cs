using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody2d;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();
        SetFallAnimation();
    }

    private bool IsGrounded()
    {
        float radius = 0.3f;

        return Physics2D.OverlapCircle(_groundCheck.position, radius, _groundLayer);
    }

    private void Jump()
    {
        const string ButtonJump = "Jump";
        const string ParameterJump = "Jump";

        if (Input.GetButtonDown(ButtonJump) && IsGrounded())
        {
            _animator.SetTrigger(ParameterJump);
            _rigidbody2d.AddForce(Vector2.up * _jumpForce);
        }
    }

    private void Move()
    {
        const string ParameterSpeed = "Speed";
        const string AxisHorizontal = "Horizontal";

        float horizontalMove = Input.GetAxisRaw(AxisHorizontal);
        _animator.SetFloat(ParameterSpeed, Mathf.Abs(horizontalMove));

        _rigidbody2d.velocity = new Vector2(horizontalMove * _movementSpeed * Time.fixedDeltaTime, _rigidbody2d.velocity.y);
        Flip(horizontalMove);
    }

    private void Flip(float horizontalMove)
    {
        if (horizontalMove < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (horizontalMove > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void SetFallAnimation()
    {
        const string ParameterFall = "Fall";

        _animator.SetBool(ParameterFall, !IsGrounded());
    }
}
