using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private float _healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            HealPlayer(player);
        }
    }

    private void HealPlayer(Player player)
    {
        if (player.CurrentHealth == player.MaxHealth)
            return;

        player.TakeHeal(_healAmount);

        Destroy(gameObject);
    }
}
