using UnityEngine;

[CreateAssetMenu(fileName = "EnemyActionSO", menuName = "Scriptable Objects/EnemyActionSO")]
public abstract class EnemyActionSO : ScriptableObject
{
    public abstract void EnemyAction(Player player, Enemy enemy);
    public abstract string GetActionName();
}
