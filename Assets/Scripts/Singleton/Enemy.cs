using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Enemy : Singleton<Enemy>
{
    public int maxHp = 10;
    public int hp = 10;
    public ActonListSO actionListSO;

    private List<EnemyActionSO> actionBuffer = new List<EnemyActionSO>();
    private EnemyActionSO currentAction;

    public EnemyActionSO test1;
    public EnemyActionSO test2;

    private void Awake()
    {
        SetupBuffer();
    }

    public void DoAction(Player player, Enemy enemy)
    {
        currentAction.EnemyAction(player, enemy);
    }

    public string GetCurrentActionName()
    {
        return currentAction.GetActionName();
    }

    public void ResetEnemyHp(int num)
    {
        maxHp = num;
        hp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        hp = hp - damage > 0 ? hp - damage : 0;
        Debug.Log($"Enemy took {damage} damage! Remaining HP: {hp}");
    }

    public void Heal(int healAmount)
    {
        hp = Mathf.Min(hp + healAmount, maxHp);
    }

    public void ChangeActon()
    {
        // change here when you add more actions
        if ( hp * 2 < maxHp && Random.value < 0.5f)
        {
            //currentAction = actionBuffer[0];
            currentAction = test1;
        }
        else
        {
            //currentAction = actionBuffer[1];
            currentAction = test2;
        }
    }

    private void SetupBuffer()
    {
        actionBuffer.Clear();
        foreach (var action in actionListSO.enemyAcitonLists)
        {
            actionBuffer.Add(action.actonScript);
        }
    }
}
