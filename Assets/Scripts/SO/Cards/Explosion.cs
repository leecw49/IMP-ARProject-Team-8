using UnityEngine;

[CreateAssetMenu(fileName = "New Explosion Card", menuName = "Cards/Explosion")]
public class Explosion : Card
{
    public int damage = 5;

    public override void UseEffect(Player player, Enemy enemy)
    {
        player.SetBaseDamage(damage);
        player.GetAttackDamage(out int attackDamage);
        enemy.TakeDamage(attackDamage);
        Debug.Log($"[Explosion Card] used! Dealt {damage} damage.");
        //throw new System.NotImplementedException();
    }
}
