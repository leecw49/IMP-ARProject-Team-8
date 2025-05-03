using UnityEngine;

[CreateAssetMenu(fileName = "Action_Attack", menuName = "EnemyActions/Action_Attack")]
public class Action_Attack : EnemyActionSO
{
    public string actionName = "Attack";

    public int damage = 3;

    public override void EnemyAction(Player player, Enemy enemy)
    {
        player.TakeDamage(damage);
    }

    public override string GetActionName()
    {
        return actionName;
        //throw new System.NotImplementedException();
    }
}
