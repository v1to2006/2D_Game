using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class CompleteArea : MonoBehaviour
{
    [SerializeField] private string _levelCompletedScene;
    [SerializeField] private Objective _objective;

    private SpriteRenderer _spriteRendrer;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _spriteRendrer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        _spriteRendrer.enabled = false;
        _boxCollider2D.enabled = false;
    }

    private void OnEnable()
    {
        _objective.EnemiesEliminated += Open;
    }

    private void OnDisable()
    {
        _objective.EnemiesEliminated -= Open;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            SceneManager.LoadScene(_levelCompletedScene);
        }
    }

    private void Open()
    {
        _spriteRendrer.enabled = true;
        _boxCollider2D.enabled = true;
    }
}
