using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private float _healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) && player.CurrentHealth < player.MaxHealth)
        {
            HealPlayer(player);
        }
    }

    private void HealPlayer(Player player)
    {
        player.TakeHeal(_healAmount);

        Destroy(gameObject);
    }
}
