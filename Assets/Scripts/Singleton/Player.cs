using UnityEngine;
using System.Collections.Generic;

public class Player : Singleton<Player>
{
    public int maxHp = 100;
    public int hp = 100;

    // Added by Yovin ===========================================================
    private int baseDamage = 2;
    private int blocking = 0;
    private float powerUpFactor = 1;

    public void TakeDamage(int damage)
    {
        if (blocking > 0)
        {
            blocking--;
            if (blocking < 0) { blocking = 0; }
            Debug.Log($"Attack blocked! Remaining blocking: {blocking}");
        }
        else
        {
            hp = hp - damage > 0 ? hp - damage : 0;
            Debug.Log($"Player took {damage} damage! Remaining HP: {hp}");
        }
    }

    // for Defence Card
    public void BlockAttack(int num)
    {
        blocking += num;
        if (blocking < 0) { blocking = 0; }
    }

    // for PowerUp Card
    public void AtkPowerUp(float num)
    {
        powerUpFactor *= num;
    }

    // for Attack Card
    public void SetBaseDamage(int num)
    {
        baseDamage = num;
    }
    public void GetAttackDamage(out int attackDamage)
    {
        attackDamage = (int)(baseDamage * powerUpFactor);
        baseDamage = 2;
        powerUpFactor = 1;
    }
}
