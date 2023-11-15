using UnityEngine;

public class Heal : MonoBehaviour
{
    private float _heal = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) && player.CurrentHealh < player.MaxHealth)
        {
            HealPlayer(player);
        }
    }

    private void HealPlayer(Player player)
    {
        player.TakeHeal(_heal);

        Destroy(gameObject);
    }
}
