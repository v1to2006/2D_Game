using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMesh : MonoBehaviour
{
    private PlayerAnimator _playerAnimator;

    public PlayerAnimator PlayerAnimator => _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
    }
}
