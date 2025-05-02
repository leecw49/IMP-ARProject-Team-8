using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Card", menuName = "Cards/Attack")]
public class AttackCard : Card
{
    public int damage = 2;

    public override void UseEffect(Player player, Enemy enemy)
    {
        player.SetBaseDamage(damage);
        player.GetAttackDamage(out int attackDamage);
        enemy.TakeDamage(attackDamage);
        Debug.Log($"[Attack Card] used! Dealt {damage} damage.");
        //throw new System.NotImplementedException();
    }
}