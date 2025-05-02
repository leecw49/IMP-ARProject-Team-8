using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    public int maxHp = 20;
    public int hp = 20;

    public Action action;
    public GameObject icon;

    public Enemy(int maxHp)
    {
        this.maxHp = maxHp;
        hp = this.maxHp;
    }

    public void TakeDamage(int damage)
    {
        hp = hp - damage > 0 ? hp - damage : 0;
        Debug.Log($"Enemy took {damage} damage! Remaining HP: {hp}");
    }

    public abstract Action NewAction();
}
