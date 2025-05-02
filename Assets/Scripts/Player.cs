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

    // 기존 코드 - 아마 이 밑으로 싹 지워야할듯? ===================================
    /*
    public Dictionary<CardType, int> cardCounts = new();
    private Dictionary<CardType, int> _cardBackup = new();

    private void Start()
    {
        cardCounts[CardType.Attack] = 2;
        cardCounts[CardType.Defense] = 2;
        cardCounts[CardType.Explosion] = 2;

        BackupCards();
    }

    public void BackupCards()
    {
        _cardBackup = new Dictionary<CardType, int>(cardCounts);
    }

    public void ResetCards()
    {
        cardCounts = new Dictionary<CardType, int>(_cardBackup);
    }

    public void AddCardReward(CardType type)
    {
        cardCounts[type]++;
    }

    public bool UseCard(CardType type)
    {
        if (cardCounts.ContainsKey(type) && cardCounts[type] > 0)
        {
            cardCounts[type]--;
            return true;
        }
        return false;
    }
    */
}
