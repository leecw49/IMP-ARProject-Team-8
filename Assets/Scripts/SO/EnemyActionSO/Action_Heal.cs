using UnityEngine;

[CreateAssetMenu(fileName = "Action_Heal", menuName = "EnemyActions/Action_Heal")]
public class Action_Heal : EnemyActionSO
{
    public string actionName = "Heal";

    public int healAmount = 3;
    public override void EnemyAction(Player player, Enemy enemy)
    {
        enemy.Heal(healAmount);
        //throw new System.NotImplementedException();
    }

    public override string GetActionName()
    {
        return actionName;
        //throw new System.NotImplementedException();
    }
}
